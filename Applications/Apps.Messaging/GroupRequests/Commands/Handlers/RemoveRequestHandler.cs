using Shared.Models;
using Apps.Messaging.GroupRequests.Commands.Models;
using Domains.Messaging.GroupRequestEntity.Repos;
using Apps.Messaging.Managers;
using Apps.Messaging.GroupRequests.Shared;

namespace Apps.Messaging.GroupRequests.Commands.Handlers;
internal sealed class RemoveRequestHandler(IGroupRequestRepo groupRequestRepo)
    : GroupRequestHandler<RemoveRequestModel , Result>(groupRequestRepo) {
    public override async Task<Result> Handle(RemoveRequestModel request , CancellationToken cancellationToken) {
        return await TryToAsync(new TryToModel(request.GroupId , request.RequesterId , EventType.Deleted) , DeleteAsync);
    }
}
