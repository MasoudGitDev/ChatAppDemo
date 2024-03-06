using Apps.Messaging.GroupAdmins.Commands.Models;
using Apps.Messaging.Shared.Manager;
using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupMemberEntity.Entity;
using Domains.Messaging.Shared.ValueObjects;
using Domains.Messaging.UnitOfWorks;
using Shared.Enums;
using Shared.Extensions;
using Shared.Models;

namespace Apps.Messaging.GroupAdmins.Commands.Handlers;
internal sealed class ConfirmGroupRequestHandler(IGroupMessagingUOW _unitOfWork)
    : GroupManager<ConfirmGroupRequestModel , Result>(_unitOfWork.ThrowIfNull()) {
    public override async Task<Result> Handle(ConfirmGroupRequestModel request , CancellationToken cancellationToken) {

        // all admins can confirm.
        ( await GetAdminMemberInfoAsync(request.GroupId , request.AdminId) )
            .ThrowIfNull("You are not admin.");

        await ConfirmRequestAsync(request.GroupId , request.RequesterId);


        return new Result(ResultStatus.Success , null);
    }

    private async Task ConfirmRequestAsync(GroupId groupId , AppUserId requesterId) {
        var groupRequest = (await GetGroupRequestAsync(groupId,requesterId))
            .ThrowIfNull($"Not found any request : [groupId : <{groupId}> , requesterId : <{requesterId}>].");
        await _unitOfWork.CreateAsync(GroupMemberTbl.Create(groupId , requesterId));
        _unitOfWork.Remove(groupRequest);
        await SaveChangesAsync();
    }
}
