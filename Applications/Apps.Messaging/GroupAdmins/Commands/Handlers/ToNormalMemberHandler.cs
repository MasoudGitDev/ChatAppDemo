using Apps.Messaging.GroupAdmins.Commands.Models;
using Apps.Messaging.Shared.Manager;
using Domains.Messaging.GroupMemberEntity.Entity;
using Domains.Messaging.UnitOfWorks;
using Shared.Exceptions;
using Shared.Extensions;
using Shared.Models;

namespace Apps.Messaging.GroupAdmins.Commands.Handlers;
internal sealed class ToNormalMemberHandler(IGroupMessagingUOW _unitOfWork)
    : GroupManager<ToNormalMemberModel , Result>(_unitOfWork.ThrowIfNull()) {
    public override async Task<Result> Handle(ToNormalMemberModel request , CancellationToken cancellationToken) {

        var targetAdmin = (await GetAdminMemberAsync(request.GroupId, request.MemberId))
            .ThrowIfNull($"Not found any target admin members with id :{request.MemberId}");

        var admin = (await GetAdminMemberAsync(request.GroupId, request.AdminId))
            .ThrowIfNull("You are not admin!");

        //very important
        if(targetAdmin.IsAdmin is false) {
            throw new NotPossibleException(
                "ToNormal methods available when the <target-member> is an admin too.");
        }

        return await ToNormalAsync(admin , targetAdmin);
    }

    private ResultMessage CreateResultMessage()
       => new("ToNormalMember" ,
               $"The target admin has been normalized successfully.");

    private async Task<Result> ToNormalAsync(
        GroupMemberTbl admin ,
        GroupMemberTbl targetMember) {
        return await UseStrategyAsync(
          admin: admin ,
          targetMember: targetMember ,
          successResultMessage: CreateResultMessage() ,
          changeOwnerWhenDeputyNeeded: () => { admin.ToNormal(); } ,
          doFinally: _unitOfWork.SaveChangesAsync ,
          levelToAssign: null
      );
    }
}
