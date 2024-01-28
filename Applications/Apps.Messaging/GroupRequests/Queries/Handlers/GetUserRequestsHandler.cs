using Apps.Messaging.GroupRequests.Queries.Models;
using Apps.Messaging.GroupRequests.Shared;
using Apps.Messaging.Managers;
using Domains.Messaging.GroupRequestEntity.Repos;
using Shared.Models;

namespace Apps.Messaging.GroupRequests.Queries.Handlers;
internal sealed class GetUserRequestsHandler(IGroupRequestRepo groupRequestRepo)
    : GroupRequestHandler<GetUserRequestsModel , Result<List<GroupRequestResult>>>(groupRequestRepo) {
    public override async Task<Result<List<GroupRequestResult>>> Handle(GetUserRequestsModel request , CancellationToken cancellationToken) {
        return await GetUserRequestsResultAsync(request.RequesterId);
    }
}
