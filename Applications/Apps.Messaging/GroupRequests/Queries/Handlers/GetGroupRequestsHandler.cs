using MediatR;
using Shared.Models;
using Shared.Enums;
using Apps.Messaging.GroupRequests.Queries.Models;
using Apps.Messaging.GroupRequests.Shared;
using Domains.Messaging.GroupRequestEntity.Repos;

namespace Apps.Messaging.GroupRequests.Queries.Handlers;
internal sealed class GetGroupRequestsHandler(IGroupRequestRepo  groupRequestRepo )
    : IRequestHandler<GetGroupRequestsModel , Result<List<GroupRequestsResult>>> {
    public async Task<Result<List<GroupRequestsResult>>> Handle(GetGroupRequestsModel request , CancellationToken cancellationToken) {
        var model = await groupRequestRepo.Queries.GetGroupRequestsAsync(request.GroupId);
        if(model == null) {
            return new Result<List<GroupRequestsResult>>(ResultStatus.Warning , new("Get" , "NotFounded" , "No any Records for this.") , null);
        }
        var dtoModels = model.Select(x=> new GroupRequestsResult(x.Description,x.RequestNumbers,x.RequestedAt,x.IsBlocked , x.RequesterId)).ToList();
        return new Result<List<GroupRequestsResult>>(ResultStatus.Success , null ,dtoModels);
    }
}
