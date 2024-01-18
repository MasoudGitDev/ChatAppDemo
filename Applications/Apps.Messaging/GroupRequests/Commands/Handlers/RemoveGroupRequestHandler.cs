using Shared.Models;
using Apps.Messaging.GroupRequests.Commands.Models;
using Domains.Messaging.GroupRequestEntity.Repos;
using Apps.Messaging.Managers;

namespace Apps.Messaging.GroupRequests.Commands.Handlers;
internal sealed class RemoveGroupRequestHandler(IGroupRequestRepo groupRequestRepo)
    : GroupRequestHandler<RemoveGroupRequestModel , Result>(groupRequestRepo) {
    public override async Task<Result> Handle(RemoveGroupRequestModel request , CancellationToken cancellationToken) {
        return await TryToAsync(request.GroupId , request.RequesterId , RemoveAsync);
    }
}
