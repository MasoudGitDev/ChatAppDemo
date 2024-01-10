using MediatR;
using Shared.Models;
using Shared.Enums;
using Apps.Messaging.GroupRequests.Commands.Models;
using Domains.Messaging.GroupEntity.Repo;

namespace Apps.Messaging.GroupRequests.Commands.Handlers;
internal sealed class RemoveGroupRequestHandler(IGroupRepo groupRepo) : IRequestHandler<RemoveGroupRequestModel , Result> {
    public async Task<Result> Handle(RemoveGroupRequestModel request , CancellationToken cancellationToken) {
        var groupRequester = await groupRepo.Queries.GetRequestAsync(request.GroupId , request.RequesterId);
        if(groupRequester == null) {
            return new Result(ResultStatus.Failed , new("Update" , "NotFounded" ,
                "There is no any record related to group and requester Ids."));
        }
        await groupRepo.Commands.RemoveRequestAsync(groupRequester);
        return new Result(ResultStatus.Success , null);
    }
}
