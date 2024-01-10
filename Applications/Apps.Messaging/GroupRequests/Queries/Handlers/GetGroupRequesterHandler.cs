using MediatR;
using Shared.Models;
using Shared.Enums;
using Apps.Messaging.GroupRequests.Queries.Models;
using Apps.Messaging.GroupRequests.Shared;
using Domains.Messaging.GroupEntity.Repo;

namespace Apps.Messaging.GroupRequests.Queries.Handlers;
internal sealed class GetGroupRequesterHandler(IGroupRepo groupRepo)
    : IRequestHandler<GetGroupRequestModel , Result<GroupRequesterResult>> {
    public async Task<Result<GroupRequesterResult>> Handle(GetGroupRequestModel request , CancellationToken cancellationToken) {
        var model = await groupRepo.Queries.GetRequestAsync(request.GroupId , request.RequesterId);
        if(model == null) {
            return new Result<GroupRequesterResult>(ResultStatus.Warning , new("Get" , "NotFounded" , "No any Records for this.") , null);
        }
        return new Result<GroupRequesterResult>(ResultStatus.Success , null , new GroupRequesterResult(model.Description , model.RequestNumbers , model.RequestedAt , model.IsBlocked));
    }
}
