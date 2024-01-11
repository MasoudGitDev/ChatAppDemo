using MediatR;
using Shared.Models;
using Shared.Enums;
using Apps.Messaging.GroupRequests.Commands.Models;
using Domains.Messaging.GroupRequestEntity.Repos;
using Apps.Messaging.GroupRequests.Shared;
namespace Apps.Messaging.GroupRequests.Commands.Handlers;
internal sealed class UpdateGroupRequestHandler(IGroupRequestRepo groupRequestRepo)
    : GroupRequestManager(groupRequestRepo) , IRequestHandler<UpdateGroupRequestModel , Result> {
    public async Task<Result> Handle(UpdateGroupRequestModel request , CancellationToken cancellationToken) {
        var groupRequest =await GetRequestWithCheckingAsync(request.GroupId,request.RequesterId);
        await UpdateAsync(request.Description , groupRequest);
        return new Result(ResultStatus.Success , null);
    }
}
