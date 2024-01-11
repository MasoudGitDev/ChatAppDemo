using MediatR;
using Shared.Models;
using Shared.Enums;
using Apps.Messaging.GroupRequests.Commands.Models;
using Domains.Messaging.GroupRequestEntity.Repos;
using Apps.Messaging.GroupRequests.Shared;

namespace Apps.Messaging.GroupRequests.Commands.Handlers;
internal sealed class CreateGroupRequestHandler(IGroupRequestRepo groupRequestRepo)
    :GroupRequestManager(groupRequestRepo) , IRequestHandler<CreateGroupRequestModel , Result> {
    public async Task<Result> Handle(CreateGroupRequestModel request , CancellationToken cancellationToken) {
        var groupRequest = await groupRequestRepo.Queries.GetRequestAsync(request.GroupId , request.RequesterId);
        if(groupRequest != null) {
            await UpdateAsync(request.Description , groupRequest); 
        }
        else {
            await CreateAsync(request.GroupId , request.RequesterId , request.Description);
        }
        return new Result(ResultStatus.Success , null);
    }
}
