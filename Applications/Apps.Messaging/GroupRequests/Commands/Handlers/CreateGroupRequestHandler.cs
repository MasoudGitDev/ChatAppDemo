using MediatR;
using Shared.Models;
using Shared.Enums;
using Apps.Messaging.GroupRequests.Commands.Models;
using Domains.Messaging.GroupEntity.Repo;
using Domains.Messaging.GroupRequestEntity;

namespace Apps.Messaging.GroupRequests.Commands.Handlers;
internal sealed class CreateGroupRequestHandler(IGroupRepo groupRepo)
    : IRequestHandler<CreateGroupRequestModel , Result> {
    public async Task<Result> Handle(CreateGroupRequestModel request , CancellationToken cancellationToken) {
        var groupRequest = await groupRepo.Queries.GetRequestAsync(request.GroupId , request.RequesterId);
        if(groupRequest != null) {
            groupRequest.RequestNumbers = groupRequest.RequestNumbers + 1;
            await groupRepo.Commands.UpdateRequestAsync(groupRequest);
        }
        else {
            await groupRepo.Commands.CreateRequestAsync(new GroupRequestTbl() {
                GroupId = request.GroupId ,
                RequesterId = request.RequesterId ,
                Id = Guid.NewGuid() ,
                RequestNumbers = 1 ,
                IsBlocked = false ,
                RequestedAt = DateTime.UtcNow ,
                Description = request.Description ,
            });
        }
        return new Result(ResultStatus.Success , null);
    }
}
