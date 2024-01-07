using Apps.Messaging.Groups.Commands.Models;
using Domains.Messaging.GroupEntity;
using Domains.Messaging.GroupEntity.Repo;
using Domains.Messaging.GroupMemberEntity;
using Domains.Messaging.GroupRequestEntity;
using Domains.Messaging.Shared.ValueObjects;
using MediatR;
using Shared.Abstractions.Messaging.Constants;
using Shared.Enums;
using Shared.Models;

namespace Apps.Messaging.Groups.Commands.Handlers;
internal sealed class CreateGroupHandler(IGroupRepo groupRepo) : IRequestHandler<CreateGroupModel , Result> {
    public async Task<Result> Handle(CreateGroupModel request , CancellationToken cancellationToken) {
        var findGroup = await groupRepo.Queries.GetGroupByDisplayIdAsync(request.DisplayId);
        if(findGroup !=null) {
            return new Result(ResultStatus.Failed , new("GetGroupByDisplayIdAsync" , "Founded" , "Your group must have a unique displayId"));
        }
        Guid groupId = Guid.NewGuid();
        DateTime startAt = DateTime.UtcNow;
        var creator = new GroupMemberTbl{
            Id = Guid.NewGuid(),
            GroupId = groupId,
            MemberId = request.CreatorId,
            IsAdmin =true,
            MemberAt = DateTime.UtcNow,
            IsBlocked = false,
            BlockMemberInfo = null,
            AdminInfo = new AdminMemberInfo(AdminAccessLevels.Creator,startAt ,null,request.CreatorId, "Group-Creator")            
        };
        var newGroup = new GroupTbl{
            GroupId = groupId,
            DisplayId = request.DisplayId,
            CreatorId = request.CreatorId,
            CreatedAt=startAt,
            Title = request.Title,           
            Description = request.Description,
            IsRequestable = request.IsRequestable,
            Categories = new() ,//
            Logos = new(),        //   
            Members = new(),//
            Requests= new List<GroupRequestTbl>()
        };
        await groupRepo.Commands.CreateGroupAsync(newGroup,creator);
        return new Result(ResultStatus.Success , null);
    }
}
