using Apps.Messaging.Groups.Commands.Models;
using Apps.Messaging.Groups.Queries.Models;
using Apps.Messaging.Shared.ResultModels;
using Domains.Messaging.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Server.WebAPI.Controllers.Shared;
using Shared.DTOs.Group;
using Shared.Models;

namespace Server.WebAPI.Controllers.Messaging
{
    [Route("Api/Messaging/[controller]")]
    [ApiController]
    [ErrorResult]
    public class GroupsController(ISender sender , ILogger<GroupsController> logger) : ControllerBase {

        [HttpGet("GetGroupsBySearchText")]
        public async Task<Result<List<GroupResultModel>>> GetGroupsBySearchTextAsync(string searchText) 
            => await sender.Send(new GetGroupsBySearchTextModel { SearchText = searchText });

        [HttpGet("GetUserGroups")]
        public async Task<Result<LinkedList<GroupResultDto>>> GetUserGroupsAsync(Guid userId)
         => await sender.Send(new GetUserGroupsModel { AppUserId = userId });

        [HttpPost("CreateGroup")]
        public async Task<Result> CreateGroupAsync([FromBody]CreateGroupModel createModel) {
            return await sender.Send(createModel);
        }

        [HttpDelete("LeaveGroup")]
        public async Task<Result> LeaveGroupAsync([FromBody]LeaveGroupModel leaveModel) {
            return await sender.Send(leaveModel);
        }

        [HttpGet("GetMembers")]
        public async Task<Result<List<MemberInfo>>> GetMembersAsync([FromQuery] GetGroupMembersModel getMembersModel) {
            return await sender.Send(getMembersModel);
        }

        [HttpPost("RequestMembership")]
        public async Task<Result> RequestMembershipAsync([FromQuery] RequestMembershipModel model) {
            return await sender.Send(model);
        }
    }
}
