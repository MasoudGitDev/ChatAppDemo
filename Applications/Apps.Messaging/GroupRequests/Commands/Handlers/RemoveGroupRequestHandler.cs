using MediatR;
using Shared.Models;
using Shared.Enums;
using Apps.Messaging.GroupRequests.Commands.Models;
using Domains.Messaging.GroupRequestEntity.Repos;
using Apps.Messaging.GroupRequests.Shared;

namespace Apps.Messaging.GroupRequests.Commands.Handlers;
internal sealed class RemoveGroupRequestHandler(IGroupRequestRepo groupRequestRepo) 
    :GroupRequestManager(groupRequestRepo) ,  IRequestHandler<RemoveGroupRequestModel , Result> {
    public async Task<Result> Handle(RemoveGroupRequestModel request , CancellationToken cancellationToken) {
        var groupRequest = await GetRequestWithCheckingAsync(request.GroupId,request.RequesterId);
        await groupRequestRepo.Commands.DeleteAsync(groupRequest);
        return new Result(ResultStatus.Success , null);
    }
}
