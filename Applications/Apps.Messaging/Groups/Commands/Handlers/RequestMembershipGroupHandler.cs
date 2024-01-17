using Apps.Messaging.Groups.Commands.Models;
using Domains.Messaging.GroupEntity.Repo;
using Domains.Messaging.GroupMemberEntity;
using Domains.Messaging.GroupMemberEntity.ValueObjects;
using Domains.Messaging.GroupRequestEntity;
using Domains.Messaging.GroupRequestEntity.ValueObjects;
using Domains.Messaging.Shared.ValueObjects;
using MediatR;
using Shared.Enums;
using Shared.Models;

namespace Apps.Messaging.Groups.Commands.Handlers;
internal sealed class RequestMembershipGroupHandler(IGroupRepo groupRepo) : IRequestHandler<RequestMembershipModel , Result> {
    public async Task<Result> Handle(RequestMembershipModel request , CancellationToken cancellationToken) {
        var findGroup = await groupRepo.Queries.GetGroupByDisplayIdAsync(request.DisplayId);
        if (findGroup == null) {
            return new Result(ResultStatus.Failed ,new("GetGroupByDisplayIdAsync" , "NotFound" , ""));
        }
        var findUser = await groupRepo.Queries.GetMemberAsync(findGroup.GroupId , request.UserId);
        if (findUser != null) {
            return new Result(ResultStatus.Failed , new("RequestMembership" , "NotPossible" , "You was member before."));
        }
        
        if(findGroup.IsRequestable) {
            var newRequest = new GroupRequestTbl{
                Id = new GroupRequestId(Guid.NewGuid()),
                GroupId = findGroup.GroupId,
                RequesterId =new AppUserId(request.UserId),
                RequestedAt = DateTime.UtcNow,
                IsBlocked = false,
                RequestNumbers = 1,  
                Description = "Hi, i like to be a member at this group"
            };
            await groupRepo.GroupRequestRepo.Commands.CreateAsync(newRequest);
        }
        else {
            var newMember = new GroupMemberTbl{
                Id = new GroupMemberId(Guid.NewGuid()),
                MemberId = new AppUserId(request.UserId),
                GroupId=findGroup.GroupId,
                IsAdmin= false,
                AdminInfo= null,
                IsBlocked= false,
                BlockMemberInfo= null,
                MemberAt = DateTime.UtcNow
            };
            await groupRepo.Commands.CerateMemberAsync(newMember);
        }        
        return new Result(ResultStatus.Success , null);
    }
}
