using Apps.Messaging.GroupAdmins.Commands.Models;
using Apps.Messaging.Shared.Manager;
using Domains.Messaging.GroupMemberEntity.Entity;
using Domains.Messaging.Shared.ValueObjects;
using Domains.Messaging.UnitOfWorks;
using Shared.Exceptions;
using Shared.Extensions;
using Shared.Models;
namespace Apps.Messaging.GroupAdmins.Commands.Handlers;
internal sealed class BlockMemberGroupHandler(IGroupMessagingUOW _unitOfWork)
    : GroupManager<BlockMemberModel , Result>(_unitOfWork.ThrowIfNull()) {
    public override async Task<Result> Handle(BlockMemberModel request , CancellationToken cancellationToken) {

        var targetMember = (await GetMemberAsync(request.GroupId, request.MemberId))
            .ThrowIfNull($"Not found any members with id :{request.MemberId}");

        var admin = (await GetAdminMemberAsync(request.GroupId, request.AdminId))
            .ThrowIfNull("You are not admin!");

        if(targetMember.IsBlocked) {
            throw new NotPossibleException("This member has been blocked before.");
        }

        return await BlockAsync(admin , targetMember , request.StartAt , request.EndAt , request.Reason);
    }


    private ResultMessage CreateResultMessage()
       => new("BlockMember" ,
               $"The <member> has been blocked successfully.");

    private async Task<Result> BlockAsync(
        GroupMemberTbl admin ,
        GroupMemberTbl targetMember ,
        DateTime? startAt ,
        DateTime? endAt ,
        string? reason
        ) {
        return await UseStrategyAsync(
          admin: admin ,
          targetMember: targetMember ,
          successResultMessage: CreateResultMessage() ,
          doFinally: async () => {
              targetMember.Block(admin.MemberId.Value , startAt , endAt , reason);
              await _unitOfWork.SaveChangesAsync();
          } ,
          changeOwnerWhenDeputyNeeded: null ,
          levelToAssign: null
        );
    }

    protected override StrategyResult CheckUserIdsEquality(GroupMemberTbl admin , AppUserId targetMemberId) {
        if(admin.MemberId.Value == targetMemberId.Value) {
            throw new NotPossibleException("You can not block yourself");
        }
        return StrategyResult.NotSameIds;
    }

}

