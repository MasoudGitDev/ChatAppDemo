using Apps.Messaging.GroupAdmins.Commands.Models;
using Apps.Messaging.Shared.Manager;
using Domains.Messaging.GroupMemberEntity.Entity;
using Domains.Messaging.UnitOfWorks;
using Shared.Abstractions.Messaging.Constants;
using Shared.Extensions;
using Shared.Models;

namespace Apps.Messaging.GroupAdmins.Commands.Handlers;
internal sealed class ToAdminMemberHandler(IGroupMessagingUOW _unitOfWork)
    : GroupManager<ToAdminMemberModel , Result>(_unitOfWork.ThrowIfNull()) {
    public override async Task<Result> Handle(ToAdminMemberModel request , CancellationToken cancellationToken) {

        var (startAt, endAt, reason, groupId, adminId, memberId, levelToAssign) = (
            request.StartAt,
            request.EndAt,
            request.Reason,
            request.GroupId,
            request.AdminId,
            request.MemberId,
            request.LevelToAssign
            );


        var targetMember = (await GetMemberAsync(groupId, memberId))
            .ThrowIfNull($"Not found any members with id :{request.MemberId}");

        var admin = (await GetAdminMemberAsync(groupId, adminId))
            .ThrowIfNull("You are not admin!");


        return await ToAdminAsync(admin , targetMember , levelToAssign , startAt , endAt , reason);
    }

    private ResultMessage CreateResultMessage()
       => new("ToAdminMember" ,
               $"The <admin-state> has been changed successfully.");

    private async Task<Result> ToAdminAsync(
        GroupMemberTbl admin ,
        GroupMemberTbl targetMember ,
        AdminLevel levelToAssign ,
        DateTime? startAt ,
        DateTime? endAt ,
        string? reason) {
        return await UseStrategyAsync(
          admin: admin ,
          targetMember: targetMember ,
          successResultMessage: CreateResultMessage() ,
          changeOwnerWhenDeputyNeeded: () => {
              admin.ToAdmin(admin.MemberId.Value , levelToAssign , startAt , endAt , reason);
          } ,
          doFinally: async () => {
              targetMember.ToAdmin(admin.MemberId.Value ,levelToAssign , startAt , endAt , reason);
              await _unitOfWork.SaveChangesAsync();
          } ,
          levelToAssign: levelToAssign
      );
    }
}
