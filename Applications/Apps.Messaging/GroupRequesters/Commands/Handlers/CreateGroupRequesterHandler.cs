using Apps.Messaging.GroupRequesters.Commands.Models;
using Domains.Messaging.GroupRequesterEntity;
using Domains.Messaging.GroupRequesterEntity.Repos;
using MediatR;
using Shared.Models;
using Shared.Enums;

namespace Apps.Messaging.GroupRequesters.Commands.Handlers;  
internal sealed class CreateGroupRequesterHandler(IGroupRequesterRepo groupRequesterRepo)
    : IRequestHandler<CreateGroupRequesterModel , Result> {
    public async Task<Result> Handle(CreateGroupRequesterModel request , CancellationToken cancellationToken) {
        var groupRequester = (await groupRequesterRepo.GetAsync(request.GroupId , request.RequesterId));
        if (groupRequester != null) {
            groupRequester.RequestNumbers = groupRequester.RequestNumbers + 1;
            await groupRequesterRepo.UpdateAsync(groupRequester);
        }
        else {
            await groupRequesterRepo.CreateAsync(new GroupRequesterTbl() {
                GroupId = request.GroupId ,
                RequesterId = request.RequesterId ,
                Id = Guid.NewGuid() ,
                RequestNumbers = 1,
                IsBlocked = false ,
                RequestedAt = DateTime.UtcNow,
                Description = request.Description ,
            });
        }           
        return new Result(ResultStatus.Success , null );
    }
}
