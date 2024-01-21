using Apps.Messaging.Exceptions;
using Apps.Messaging.GroupMessages.Commands.Models;
using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupMemberEntity.Repos;
using Domains.Messaging.GroupMessageEntity;
using Domains.Messaging.GroupMessageEntity.ValueObjects;
using Domains.Messaging.Shared.ValueObjects;
using MediatR;
using Shared.Enums;
using Shared.Models;

namespace Apps.Messaging.Managers;
internal abstract class GroupMessageHandler<T, R>(IGroupAdminRepo groupAdminRepo)
    : BaseGroupManager<T , R>(groupAdminRepo), IRequestHandler<T , R> where T : IRequest<R> where R : IResult {

    //Checking
    protected async Task CheckSendingAccessAsync(GroupId groupId , AppUserId memberId) {
        var group =  await GetGroupWithCheckingAsync(groupId);
        if(group.MessageBlocking.IsBlocked) {
            var findAdmin = GetAdminWithCheckingAsync(groupId , memberId);
            if(findAdmin != null) {
                throw new GroupMessageHandlerException(nameof(CreateAsync) , "BlockMessage" , "Just Admins can send any messages.");
            }
        }
    }
    protected async Task<GroupMessageTbl> GetMessageWithCheckingAsync(GroupMessageId groupMessageId) {
        var findMessage = await groupAdminRepo.MessageRepo.Queries.GetMessageByIdAsync(groupMessageId);
        if(findMessage == null) {
            throw new GroupMessageHandlerException("GetMessageByIdAsync" , "NotFound" , "There is no any messages with this groupMessageId");
        }
        return findMessage;
    }

    // Queries
    protected async Task<List<GroupMessageTbl>> GetGroupMessagesAsync(GroupId groupId , int messageCount = 50) {
        return await groupAdminRepo.MessageRepo.Queries.GetGroupMessagesAsync(groupId , messageCount);
    }
    protected async Task<List<GroupMessageTbl>> GetMemberMessagesAsync(GroupId groupId , AppUserId memberId) {
        return await groupAdminRepo.MessageRepo.Queries.GetMemberMessagesByGroupIdAsync(groupId , memberId);
    }
    protected async Task<GroupMessageTbl?> GetMessageAsync(GroupMessageId messageId) {
        return await groupAdminRepo.MessageRepo.Queries.GetMessageByIdAsync(messageId);
    }

    // Commands
    public async Task<Result> CreateAsync(CreateMessageModel model) {
        var message = new GroupMessageTbl{
            AppUserId = model.MemberId,
            GroupId = model.GroupId,
            Id = new GroupMessageId(),
            Message = model.Message,
            FirstChecked = false,
            LastChecked = false,
            FilePath = model.FileUrl
        };
        await groupAdminRepo.MessageRepo.Commands.CreateAsync(message);
        return new Result(ResultStatus.Success , new(nameof(UpdateAsync) , "Success" , "Creation was successful."));
    }
    public async Task<Result> UpdateAsync(UpdateMessageModel model) {
        var findMessage = await GetMessageWithCheckingAsync(model.MessageId);
        findMessage.Message = model.Message;
        // Must check the url and found it later
        if(model.FileUrl != null) {
            findMessage.FilePath = model.FileUrl;
        }
        await groupAdminRepo.MessageRepo.Commands.UpdateAsync(findMessage);
        return new Result(ResultStatus.Success , new(nameof(UpdateAsync) , "Success" , "Update was successful."));
    }

    public async Task<Result> RemoveAsync(RemoveMessageModel model) {
        var findMessage = await GetMessageWithCheckingAsync(model.MessageId);
        await groupAdminRepo.MessageRepo.Commands.UpdateAsync(findMessage);
        return new Result(ResultStatus.Success , new(nameof(UpdateAsync) , "Success" , "Deletion was successful."));
    }


    protected async Task<Result> TryToAsync(GroupMessageId groupMessageId , Func<GroupMessageTbl , Task<Result>> actions) {
        var findMessage = await GetMessageWithCheckingAsync(groupMessageId);
        return await actions(findMessage);
    }
}
