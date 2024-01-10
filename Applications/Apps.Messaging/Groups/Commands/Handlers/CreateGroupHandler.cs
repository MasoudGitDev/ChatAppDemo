using Apps.Messaging.Groups.Commands.Models;
using Domains.Messaging.GroupEntity;
using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupMemberEntity;
using Domains.Messaging.GroupMemberEntity.Repos;
using Domains.Messaging.GroupMemberEntity.ValueObjects;
using Domains.Messaging.GroupRequestEntity;
using Domains.Messaging.Shared.ValueObjects;
using MediatR;
using Shared.Abstractions.Messaging.Constants;
using Shared.Enums;
using Shared.Models;
using Shared.ValueObjects;

namespace Apps.Messaging.Groups.Commands.Handlers;
internal sealed class CreateGroupHandler(IGroupAdminRepo groupAdminRepo) : IRequestHandler<CreateGroupModel , Result> {
    public async Task<Result> Handle(CreateGroupModel request , CancellationToken cancellationToken) {
        try {
            var findGroup = await groupAdminRepo.General.Queries.GetGroupByDisplayIdAsync(request.DisplayId);
            if(findGroup != null) {
                return new Result(ResultStatus.Failed , new("GetGroupByDisplayIdAsync" , "Founded" , "Your group must have a unique displayId"));
            }
            GroupId groupId = new GroupId(Guid.NewGuid());
            DateTime startAt = DateTime.UtcNow;
            EntityId appUserId = new AppUserId(request.CreatorId);
            var newGroup = new GroupTbl{
                GroupId = groupId,
                DisplayId = new DisplayId(request.DisplayId),
                CreatorId = appUserId,
                CreatedAt=startAt,
                Title = request.Title,
                Description = request.Description,
                IsRequestable = request.IsRequestable,
                Categories = new() ,//
                Logos = new(),        //   
                Members = new(),//
                Requests= new List<GroupRequestTbl>()
            };
            await groupAdminRepo.General.Commands.CreateGroupAsync(newGroup);

            
            var creator = new GroupMemberTbl{
                Id = new GroupMemberId(Guid.NewGuid()),
                GroupId = groupId,
                MemberId = appUserId,
                IsAdmin =true,
                MemberAt = DateTime.UtcNow,
                IsBlocked = false,
                BlockMemberInfo = null,
                AdminInfo = new AdminMemberInfo(AdminAccessLevels.Creator,startAt ,null,appUserId.Value, "Group-Creator")
            };
            await groupAdminRepo.Commands.CreateMemberAsync(creator);
            return new Result(ResultStatus.Success , null);
        }
        catch (Exception ex) {
            return new Result(ResultStatus.Failed , new("CreateGroupHandler" , ex.Message.ToString() , ""));
        }
    }
}
