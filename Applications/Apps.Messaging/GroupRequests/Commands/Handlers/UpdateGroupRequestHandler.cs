using MediatR;
using Shared.Models;
using Shared.Enums;
using Apps.Messaging.GroupRequests.Commands.Models;
using Domains.Messaging.GroupEntity.Repo;
namespace Apps.Messaging.GroupRequests.Commands.Handlers;
internal sealed class UpdateGroupRequestHandler(IGroupRepo groupRepo)
    : IRequestHandler<UpdateGroupRequestModel , Result> {
    public async Task<Result> Handle(UpdateGroupRequestModel request , CancellationToken cancellationToken) {
        var groupRequest = await groupRepo.Queries.GetRequestAsync(request.GroupId , request.RequesterId);
        if(groupRequest == null) {
            return new Result(ResultStatus.Failed , new("Update" , "NotFounded" ,
                "There is no any record related to group and requester Ids."));
        }
        if(
            request.Description.Trim().Length <= 2 || string.IsNullOrWhiteSpace(request.Description) ||
            groupRequest.Description == request.Description) {
            return new Result(ResultStatus.Failed , new("Update" , "SameOrEmpty" ,
                "The description is empty or same!"));
        }
        groupRequest.RequestNumbers = groupRequest.RequestNumbers + 1;
        groupRequest.Description = request.Description;
        await groupRepo.Commands.UpdateRequestAsync(groupRequest);
        return new Result(ResultStatus.Success , null);
    }
}
