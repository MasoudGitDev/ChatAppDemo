using Apps.Messaging.Group.Commands.Models;
using Apps.Messaging.GroupAdmins.Manager;
using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupMemberEntity.Entity;
using Domains.Messaging.Shared.ValueObjects;
using Domains.Messaging.UnitOfWorks;
using Shared.Abstractions.Messaging.Constants;
using Shared.Enums;
using Shared.Exceptions;
using Shared.Extensions;
using Shared.Models;
namespace Apps.Messaging.Group.Commands.Handlers;
internal sealed class LeaveGroupHandler(IGroupMessagingUOW _unitOfWork)
    : GroupManager<LeaveGroupModel , Result>(_unitOfWork.ThrowIfNull()) {
    public override async Task<Result> Handle(LeaveGroupModel request , CancellationToken cancellationToken) {
        _unitOfWork.Remove(await GetMemberIfNotOwnerAsync(request.GroupId , request.MemberId));
        await SaveChangesAsync();
        return new Result(ResultStatus.Success , new("LeaveGroup" , "You leaved the group successfully"));
    }

    //========================
    private async Task<GroupMemberTbl> GetMemberIfNotOwnerAsync(GroupId groupId , AppUserId memberId) {
        var findMember = (await GetMemberAsync(groupId, memberId))
            .ThrowIfNull("You are not a <member>.");
        if(findMember.AdminInfo?.AdminLevel is AdminLevel.Owner) {
            throw new NotPossibleException(
                "You as an owner can not leave the group." +
                "If you want to leave, change your state to normal user or other admins level.");
        }
        return findMember;
    }

}
