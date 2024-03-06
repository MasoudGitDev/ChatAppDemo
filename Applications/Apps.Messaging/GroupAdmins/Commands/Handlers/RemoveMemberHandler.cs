using Apps.Messaging.GroupAdmins.Commands.Models;
using Apps.Messaging.Shared.Manager;
using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupMemberEntity.Entity;
using Domains.Messaging.UnitOfWorks;
using Shared.Abstractions.Messaging.Constants;
using Shared.Exceptions;
using Shared.Extensions;
using Shared.Models;
namespace Apps.Messaging.GroupAdmins.Commands.Handlers;
internal sealed class RemoveMemberHandler(IGroupMessagingUOW _unitOfWork)
    : GroupManager<RemoveMemberModel , Result>(_unitOfWork.ThrowIfNull()) {
    public override async Task<Result> Handle(RemoveMemberModel request , CancellationToken cancellationToken) {

        var targetMember = (await GetAdminMemberAsync(request.GroupId, request.MemberId))
            .ThrowIfNull($"Not found any target admin members with id :{request.MemberId}");

        var admin = (await GetAdminMemberAsync(request.GroupId, request.AdminId))
            .ThrowIfNull("You are not admin!");

        var adminLevel = admin.AdminInfo!.AdminLevel;
        if(adminLevel != AdminLevel.Owner || adminLevel != AdminLevel.Deputy) {
            throw new NotAccessException("You have not permission to remove any member.");
        }

        return await RemoveAsync(admin , targetMember);
    }

    private ResultMessage CreateResultMessage(GroupId groupId , Guid memberId)
        => new("RemoveMember" ,
                $"The member : {memberId} was removed from group : {groupId} successfully.");

    private async Task<Result> RemoveAsync(GroupMemberTbl admin , GroupMemberTbl targetMember) {
        return await UseStrategyAsync(
          admin: admin ,
          targetMember: targetMember ,
          successResultMessage: CreateResultMessage(admin.GroupId , targetMember.MemberId) ,
          changeOwnerWhenDeputyNeeded: () => { admin.ToNormal(); } ,
          doFinally: async () => {
              _unitOfWork.Remove(targetMember);
              await _unitOfWork.SaveChangesAsync();
          } ,
          levelToAssign: null
      );
    }
}
