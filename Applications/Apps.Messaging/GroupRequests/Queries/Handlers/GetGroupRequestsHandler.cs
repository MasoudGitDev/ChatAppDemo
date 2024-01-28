using Apps.Messaging.GroupRequests.Queries.Models;
using Apps.Messaging.GroupRequests.Shared;
using Apps.Messaging.Managers;
using Domains.Messaging.GroupRequestEntity.Repos;
using Shared.Models;

namespace Apps.Messaging.GroupRequests.Queries.Handlers;
internal sealed class GetGroupRequestsHandler(IGroupRequestRepo groupRequestRepo)
    : GroupRequestHandler<GetGroupRequestsModel , Result<List<GroupRequestResult>>>(groupRequestRepo) {
    public override async Task<Result<List<GroupRequestResult>>> Handle(GetGroupRequestsModel request , CancellationToken cancellationToken) {
        return await GetGroupRequestsResultAsync(request.GroupId);
    }
}
