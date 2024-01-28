using Apps.Messaging.GroupMessages.Commands.Models;
using Domains.Messaging.GroupMessageEntity;
using Domains.Messaging.GroupMessageEntity.ValueObjects;
using Shared.Enums;
using Shared.Models;

namespace Apps.Messaging.GroupMessages.Manager;

// GroupMessage CRUD
internal abstract partial class GroupMessageHandler<T, R> {

    public async Task<Result> CreateAsync(CreateMessageModel model) {
        var message = new GroupMessageTbl
        {
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
