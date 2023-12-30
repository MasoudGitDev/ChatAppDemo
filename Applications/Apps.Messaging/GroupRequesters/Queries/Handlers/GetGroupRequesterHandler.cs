using Apps.Messaging.GroupRequesters.Queries.Models;
using Domains.Messaging.GroupRequesterEntity.Repos;
using MediatR;
using Shared.Models;
using Shared.Enums;
using Apps.Messaging.GroupRequesters.Shared;

namespace Apps.Messaging.GroupRequesters.Queries.Handlers;
internal sealed class GetGroupRequesterHandler(IGroupRequesterRepo groupRequesterRepo)
    : IRequestHandler<GetGroupRequesterModel , Result<GroupRequesterResult>> {
    public async Task<Result<GroupRequesterResult>> Handle(GetGroupRequesterModel request , CancellationToken cancellationToken) {
        var model = await groupRequesterRepo.GetAsync(request.GroupId , request.RequesterId);
        if(model == null) {
            return new Result<GroupRequesterResult>(ResultStatus.Warning , new("Get" , "NotFounded" , "No any Records for this."),null);
        }
        return new Result<GroupRequesterResult>(ResultStatus.Success , null , new GroupRequesterResult(model.Description , model.RequestNumbers , model.RequestedAt , model.IsBlocked));
    }
}
