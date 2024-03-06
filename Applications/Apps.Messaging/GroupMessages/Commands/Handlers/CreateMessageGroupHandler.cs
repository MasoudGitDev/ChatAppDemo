using Apps.Messaging.GroupAdmins.Manager;
using Apps.Messaging.GroupMessages.Commands.Models;
using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupMessageEntity.Aggregate;
using Domains.Messaging.Shared.ValueObjects;
using Domains.Messaging.UnitOfWorks;
using Shared.Enums;
using Shared.Exceptions;
using Shared.Extensions;
using Shared.Models;

namespace Apps.Messaging.GroupMessages.Commands.Handlers;
internal sealed class CreateMessageGroupHandler(IGroupMessagingUOW _unitOfWork)
    : GroupManager<CreateMessageModel , Result>(_unitOfWork.ThrowIfNull()) {
    public override async Task<Result> Handle(CreateMessageModel request , CancellationToken cancellationToken) {

        var (groupId, memberId) = (request.GroupId, request.MemberId);

        await CheckGroupCondition(groupId);
        await CheckMemberCondition(groupId , memberId);

        await _unitOfWork.CreateAsync(GroupMessageTbl.Create(request));
        await _unitOfWork.SaveChangesAsync();

        return new Result(ResultStatus.Success , new("CreateMessage" , "The message has been created successfully."));
    }

    private async Task CheckGroupCondition(GroupId groupId) {
        var group = (await GetGroupAsync(groupId))
            .ThrowIfNull($"There is no any group with this id : <{groupId}> !");

        if(group.MessageLocking.IsLock) {
            string msg = group.MessageLocking.EndAt is null ?
                "Try later !" :
                $"Try after <{group.MessageLocking.EndAt}>";
            throw new NotAccessException("The group has been locked. " + msg);
        }
    }
    private async Task CheckMemberCondition(GroupId groupId , AppUserId memberId) {
        var member = (await GetMemberAsync(groupId,memberId))
            .ThrowIfNull("You are not a member!");

        if(member.IsBlocked) {
            throw new NotAccessException("The Blocked Members can not send any messages to group.");
        }
    }
}
