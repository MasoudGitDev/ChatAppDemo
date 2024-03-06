using Apps.Messaging.GroupAdmins.Manager;
using Apps.Messaging.GroupRequests.Commands.Models;
using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupRequestEntity;
using Domains.Messaging.Shared.ValueObjects;
using Domains.Messaging.UnitOfWorks;
using Shared.Enums;
using Shared.Exceptions;
using Shared.Extensions;
using Shared.Models;

namespace Apps.Messaging.GroupRequests.Commands.Handlers;
internal sealed class RequestMembershipHandler(IGroupMessagingUOW _unitOfWork)
    : GroupManager<RequestMembershipModel , Result>(_unitOfWork.ThrowIfNull()) {
    public override async Task<Result> Handle(RequestMembershipModel request , CancellationToken cancellationToken) {
        return await CreateOrUpdateAsync(request.GroupId , request.UserId , request.Description);
    }

    private async Task<Result> CreateOrUpdateAsync(GroupId groupId , AppUserId requesterId , string? description) {

        ( await GetMemberAsync(groupId , requesterId) )
            .ThrowIfFound("The members can not send a membership request again.");

        var groupRequest = await GetRequestAsync(groupId , requesterId);
        (string code, string eventName) = ("Create", "<created>");
        if(groupRequest is null) {
            await CreateAsync(groupId , requesterId , description);
        }
        else {
            (code, eventName) = ("Update", "<updated>");
            Update(groupRequest , description);
        }
        await SaveChangesAsync();
        return new Result(ResultStatus.Success , new(code , $"The request has been {eventName} successfully."));
    }

    private async Task CreateAsync(Guid groupId , Guid requesterId , string? description) {
        await _unitOfWork.CreateAsync(GroupRequestTbl.Create(
            groupId , requesterId , description));
    }

    private void Update(GroupRequestTbl groupRequest , string? newDescription) {
        if(groupRequest.IsBlocked) {
            throw new NotPossibleException("Sorry,Your latest request has been blocked and you can not send it again.");
        }
        groupRequest.Update(newDescription);
    }
}
