using Apps.Messaging.GroupRequests.Queries.Models;
using Apps.Messaging.GroupRequests.Shared;
using Domains.Messaging.GroupRequestEntity.Repos;
using MediatR;
using Shared.Enums;
using Shared.Models;

namespace Apps.Messaging.GroupRequests.Queries.Handlers;
internal sealed class GetUserRequestsHandler(IGroupRequestRepo groupRequestRepo)
    : IRequestHandler<GetUserRequestsModel , Result<List<GroupRequestsResult>>> {
    public async Task<Result<List<GroupRequestsResult>>> Handle(GetUserRequestsModel request , CancellationToken cancellationToken) {
        var userRequests = await groupRequestRepo.Queries.GetUserRequestsAsync(request.RequesterId);
        var dtoModels = userRequests.Select(x=> new GroupRequestsResult(x.Description,x.RequestNumbers,x.RequestedAt,x.IsBlocked,x.GroupId)).ToList();
        return new Result<List<GroupRequestsResult>>(ResultStatus.Success, null, dtoModels);
    }
}
