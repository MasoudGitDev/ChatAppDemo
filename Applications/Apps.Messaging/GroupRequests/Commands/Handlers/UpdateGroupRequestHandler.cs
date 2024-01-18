using Shared.Models;
using Apps.Messaging.GroupRequests.Commands.Models;
using Domains.Messaging.GroupRequestEntity.Repos;
using Apps.Messaging.Managers;
namespace Apps.Messaging.GroupRequests.Commands.Handlers;
internal sealed class UpdateGroupRequestHandler(IGroupRequestRepo groupRequestRepo)
    : GroupRequestHandler<UpdateGroupRequestModel , Result>(groupRequestRepo){
    public override async Task<Result> Handle(UpdateGroupRequestModel request , CancellationToken cancellationToken) {
        return await TryToAsync(request.GroupId ,request.RequesterId ,
            async (groupRequest) => await UpdateAsync(request.Description , groupRequest));
    }
}
