using Apps.Messaging.Group.Commands.Models;
using Apps.Messaging.Group.Queries.Models;
using Apps.Messaging.Shared.ResultModels;
using Domains.Messaging.Shared.Models;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.WebAPI.Controllers.Shared;
using Server.WebAPI.DTOs;
using Shared.DTOs.Group;
using Shared.Models;

namespace Server.WebAPI.Controllers.Messaging {
    [Route("Api/Messaging/[controller]")]
    [ApiController]
    [ErrorResult]
    [Authorize]
    public class GroupsController(ISender sender , ILogger<GroupsController> logger) : AuthController {

        // ============================== Commands

        [HttpPost("Create")]
        public async Task<Result> CreateAsync([FromForm] CreateGroupDto createDto) {
            var newGroup = createDto.Adapt<CreateGroupModel>();
            newGroup.CreatorId = GetUserId();
            return await sender.Send(newGroup);
        }

        [HttpDelete("Leave/{groupId:guid}")]
        public async Task<Result> LeaveAsync([FromRoute] Guid groupId) {
            return await sender.Send(new LeaveGroupModel { GroupId = groupId , MemberId = GetUserId() });
        }

        // ==================== Queries

        [HttpGet]
        public async Task<Result<LinkedList<GroupResultDto>>> GetMyGroupsAsync()
         => await sender.Send(new GetUserGroupsModel { AppUserId = GetUserId() });

        [HttpGet("FindByDisplayId/{displayId}")]
        public async Task<Result<GroupResultModel>> FindByDisplayIdAsync([FromRoute] string displayId)
            => await sender.Send(new FindGroupByDisplayIdModel { DisplayId = displayId });

        [HttpGet("FindByTitle/{title}")]
        public async Task<Result<List<GroupResultModel>>> FindByTitleAsync([FromRoute] string title)
           => await sender.Send(new FindGroupsByTitleModel { Title = title });

        [HttpGet("Members/{groupId:guid}")]
        public async Task<Result<List<MemberInfo>>> GetMembersAsync([FromRoute] Guid groupId) {
            return await sender.Send(new GetGroupMembersModel { GroupId = groupId });
        }


    }
}
