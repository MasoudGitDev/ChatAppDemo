using Shared.Models;
using Apps.Messaging.GroupRequests.Commands.Models;
using Domains.Messaging.GroupRequestEntity.Repos;
using Apps.Messaging.Managers;

namespace Apps.Messaging.GroupRequests.Commands.Handlers;
internal sealed class CreateGroupRequestHandler(IGroupRequestRepo groupRequestRepo)
    : GroupRequestHandler<CreateGroupRequestModel , Result>(groupRequestRepo) {
    public override async Task<Result> Handle(CreateGroupRequestModel request , CancellationToken cancellationToken) {
        return await CreateOrUpdateAsync(request.GroupId , request.RequesterId , request.Description);
    }
}
