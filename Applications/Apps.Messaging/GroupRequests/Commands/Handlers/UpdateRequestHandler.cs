using Apps.Messaging.GroupRequests.Commands.Models;
using Apps.Messaging.GroupRequests.Shared;
using Apps.Messaging.Managers;
using Domains.Messaging.GroupRequestEntity.Repos;
using Shared.Models;
namespace Apps.Messaging.GroupRequests.Commands.Handlers;
internal sealed class UpdateRequestHandler(IGroupRequestRepo groupRequestRepo)
    : GroupRequestHandler<UpdateRequestModel , Result>(groupRequestRepo) {
    public override async Task<Result> Handle(UpdateRequestModel request , CancellationToken cancellationToken) {
        return await TryToAsync(new TryToModel(request.GroupId , request.RequesterId , EventType.Updated) ,
            async (groupRequest) => await UpdateAsync(request.Description , groupRequest));
    }
}