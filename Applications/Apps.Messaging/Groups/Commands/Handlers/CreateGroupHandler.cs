using Apps.Messaging.Groups.Commands.Models;
using Domains.Messaging.GroupEntity;
using Domains.Messaging.GroupEntity.Exceptions;
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
            GroupId groupId = new GroupId();
            DateTime startAt = DateTime.UtcNow;
            EntityId appUserId = request.CreatorId;
            var newGroup = new GroupTbl{
                GroupId = groupId,
                DisplayId = new DisplayId(request.DisplayId),
                CreatorId = appUserId,
                CreatedAt=startAt,
                Title = request.Title,
                Description = request.Description,
                IsRequestable = request.IsRequestable,
                Categories = new() ,//
                LogoURLs = new(),        //   
                Members = new(),//
                Requests= new List<GroupRequestTbl>(),
                MessageBlocking = new(),
            };
            await groupAdminRepo.General.Commands.CreateGroupAsync(newGroup);

            
            var creator = new GroupMemberTbl{
                Id = new GroupMemberId(),
                GroupId = groupId,
                MemberId = appUserId,
                IsAdmin =true,
                MemberAt = DateTime.UtcNow,
                IsBlocked = false,
                BlockMemberInfo = null,
                AdminInfo = new AdminMemberInfo(AdminAccessLevels.Owner,startAt ,null,appUserId.Value, "Group-Creator")
            };
            await groupAdminRepo.Commands.CreateMemberAsync(creator);
            return new Result(ResultStatus.Success , new("CreateGroupHandler" , "Created" , "The Group created successfully."));
        }
        catch (Exception ex) {
            Console.WriteLine("CreateGroupHandler Exception:" + ex.InnerException?.Message);
            throw new GroupException("CreateGroupHandler" , ex.GetType().Name , ex.Message.ToString() + " : " + ex.InnerException?.Message);            
        }
    }
}
