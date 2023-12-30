using Apps.Messaging.GroupRequesters.Commands.Models;
using Domains.Messaging.GroupRequesterEntity.Repos;
using MediatR;
using Shared.Models;

namespace Apps.Messaging.GroupRequesters.Commands.Handlers;
internal sealed class RemoveGroupRequesterHandler(IGroupRequesterRepo groupRequesterRepo) : IRequestHandler<RemoveGroupRequesterModel , Result> {
    public async Task<Result> Handle(RemoveGroupRequesterModel request , CancellationToken cancellationToken) {
        var groupRequester = (await groupRequesterRepo.GetAsync(request.GroupId , request.RequesterId));
        if(groupRequester == null) {
            return new Result(Shared.Enums.ResultStatus.Failed , new("Update" , "NotFounded" ,
                "There is no any record related to group and requester Ids."));
        }
        await groupRequesterRepo.RemoveAsync(groupRequester);
        return new Result(Shared.Enums.ResultStatus.Success , null);
    }
}
