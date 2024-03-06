using Apps.Messaging.GroupAdmins.Manager;
using Apps.Messaging.GroupRequests.Queries.Models;
using Apps.Messaging.GroupRequests.Shared;
using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.UnitOfWorks;
using Mapster;
using Shared.Enums;
using Shared.Models;
using Shared.ValueObjects;

namespace Apps.Messaging.GroupRequests.Queries.Handlers;
internal sealed class GetGroupRequestsHandler(IGroupMessagingUOW _unitOfWork)
    : GroupManager<GetGroupRequestsModel , Result<List<GroupRequestResult>>>(_unitOfWork) {
    public override async Task<Result<List<GroupRequestResult>>> Handle(
        GetGroupRequestsModel request ,
        CancellationToken cancellationToken) {
        return await GetGroupRequestsResultAsync(request.GroupId , request.IsVisible);
    }

    private async Task<Result<List<GroupRequestResult>>> GetGroupRequestsResultAsync(GroupId groupId ,Visibility isVisible) {
        var groupRequests = await GetGroupRequestsAsync(groupId , isVisible);
        TypeAdapterConfig<EntityId , Guid>.NewConfig().MapWith(x => x.Value);
        return new Result<List<GroupRequestResult>>(
            ResultStatus.Success ,
            new("GroupRequests" , $"Group has <{groupRequests.Count()}> requests.") ,
            groupRequests.Adapt<List<GroupRequestResult>>());
    }
}