using Apps.Messaging.GroupAdmins.Commands.Models;
using Apps.Messaging.Shared.Manager;
using Domains.Messaging.GroupEntity.Entity;
using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.Shared.ValueObjects;
using Domains.Messaging.UnitOfWorks;
using Shared.Abstractions.Messaging.Constants;
using Shared.Enums;
using Shared.Exceptions;
using Shared.Extensions;
using Shared.Models;

namespace Apps.Messaging.GroupAdmins.Commands.Handlers;
internal sealed class DeleteGroupHandler(IGroupMessagingUOW _unitOfWork)
    : GroupManager<RemoveGroupModel , Result>(_unitOfWork.ThrowIfNull()) {
    public override async Task<Result> Handle(RemoveGroupModel request , CancellationToken cancellationToken) {
        var currentGroup = await GetGroupIfExistAsync(request.GroupId);
        await CheckIsOwnerAsync(request.GroupId , request.OwnerId);
        await DeleteGroupAsync(currentGroup);
        return new Result(ResultStatus.Success , new("Delete" , $"The group has been removed successfully."));
    }

    private async Task<GroupTbl> GetGroupIfExistAsync(GroupId groupId)
       => ( await GetGroupAsync(groupId) )
           .ThrowIfNull($"Not found any group with groupId : <{groupId}> .");
    private async Task CheckIsOwnerAsync(GroupId groupId , AppUserId userId) {
        var findAdminInfo = (await GetAdminMemberInfoAsync(groupId,userId))
            .ThrowIfNull("You are not admin!");

        if(findAdminInfo.AdminLevel is not AdminLevel.Owner) {
            throw new NotAccessException("Just the <owner> can <delete> the group.");
        }
    }
    private async Task DeleteGroupAsync(GroupTbl currentGroup) {
        _unitOfWork.Remove(currentGroup);
        _unitOfWork.RemoveRange(await GetMembersAsync(currentGroup.GroupId));
        _unitOfWork.RemoveRange(await GetGroupRequestsAsync(currentGroup.GroupId));
        await SaveChangesAsync();
    }

}
