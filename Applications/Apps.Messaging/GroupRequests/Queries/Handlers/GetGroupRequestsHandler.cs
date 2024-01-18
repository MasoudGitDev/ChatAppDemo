using MediatR;
using Shared.Models;
using Shared.Enums;
using Apps.Messaging.GroupRequests.Queries.Models;
using Apps.Messaging.GroupRequests.Shared;
using Domains.Messaging.GroupRequestEntity.Repos;
using Mapster;

namespace Apps.Messaging.GroupRequests.Queries.Handlers;
internal sealed class GetGroupRequestsHandler(IGroupRequestRepo  groupRequestRepo )
    : IRequestHandler<GetGroupRequestsModel , Result<List<GroupRequestsResult>>> {
    public async Task<Result<List<GroupRequestsResult>>> Handle(GetGroupRequestsModel request , CancellationToken cancellationToken) {
        var models = await groupRequestRepo.Queries.GetGroupRequestsAsync(request.GroupId); 
        var dtoModels = models.Select(x=> new GroupRequestsResult(x.Description,x.RequestNumbers,x.RequestedAt,x.IsBlocked , x.RequesterId)).ToList();
        return new Result<List<GroupRequestsResult>>(ResultStatus.Success , null ,dtoModels);
    }
}
