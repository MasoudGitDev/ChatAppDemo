using Apps.Messaging.GroupRequesters.Commands.Models;
using Domains.Messaging.GroupRequesterEntity.Repos;
using MediatR;
using Shared.Models;

namespace Apps.Messaging.GroupRequesters.Commands.Handlers;  
internal sealed class UpdateGroupRequesterHandler(IGroupRequesterRepo groupRequesterRepo)
    : IRequestHandler<UpdateGroupRequesterModel , Result> {
    public async Task<Result> Handle(UpdateGroupRequesterModel request , CancellationToken cancellationToken) {
        var groupRequester = (await groupRequesterRepo.GetAsync(request.GroupId , request.RequesterId));
        if(groupRequester == null) {
            return new Result(Shared.Enums.ResultStatus.Failed , new("Update" , "NotFounded" ,
                "There is no any record related to group and requester Ids."));
        }
        if(
            request.Description.Trim().Length <= 2 || String.IsNullOrWhiteSpace(request.Description) ||
            groupRequester.Description == request.Description )                 
        {
            return new Result(Shared.Enums.ResultStatus.Failed , new("Update" , "SameOrEmpty" ,
                "The description is empty or same!"));
        }
        groupRequester.RequestNumbers = groupRequester.RequestNumbers + 1;
        groupRequester.Description = request.Description;
        await groupRequesterRepo.UpdateAsync(groupRequester);
        return new Result(Shared.Enums.ResultStatus.Success , null);
    }
}
