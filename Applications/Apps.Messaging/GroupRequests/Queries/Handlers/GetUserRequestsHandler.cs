using Apps.Messaging.GroupRequests.Queries.Models;
using Apps.Messaging.GroupRequests.Shared;
using Apps.Messaging.Shared.Manager;
using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.Shared.ValueObjects;
using Domains.Messaging.UnitOfWorks;
using Mapster;
using Shared.Enums;
using Shared.Models;

namespace Apps.Messaging.GroupRequests.Queries.Handlers;
internal sealed class GetUserRequestsHandler(IGroupMessagingUOW _unitOfWork)
    : GroupManager<GetUserRequestsModel , Result<List<GroupRequestResult>>>(_unitOfWork) {
    public override async Task<Result<List<GroupRequestResult>>> Handle(
        GetUserRequestsModel request ,
        CancellationToken cancellationToken) {
        return await GetUserRequestsResultAsync(request.RequesterId , request.Visibility);
    }

    private async Task<Result<List<GroupRequestResult>>> GetUserRequestsResultAsync(AppUserId requesterId , Visibility visibility) {
        var userRequests = await GetUserRequestsAsync(requesterId , visibility);
        TypeAdapterConfig<GroupId , Guid>.NewConfig().MapWith(x => x.Value);
        return new Result<List<GroupRequestResult>>(
            ResultStatus.Success ,
            new("UserRequests" , $"You have sent <{userRequests.Count()}> requests to groups.") ,
            userRequests.Adapt<List<GroupRequestResult>>());
    }
}
