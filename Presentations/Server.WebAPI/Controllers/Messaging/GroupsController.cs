using Apps.Messaging.Groups.Commands.Models;
using Apps.Messaging.Groups.Queries.Models;
using Domains.Messaging.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace Server.WebAPI.Controllers.Messaging
{
    [Route("Api/Messaging/[controller]")]
    [ApiController]
    public class GroupsController(ISender sender) : ControllerBase {
        [HttpPost("Create")]
        public async Task<Result> CreateAsync([FromBody]CreateGroupModel createModel) {
            return await sender.Send(createModel);
        }

        [HttpDelete("Leave")]
        public async Task<Result> LeaveAsync([FromBody]LeaveGroupModel leaveModel) {
            return await sender.Send(leaveModel);
        }

        [HttpGet("GetMembers")]
        public async Task<Result<List<MemberInfo>>> GetMembersAsync([FromQuery] GetGroupMembersModel getMembersModel) {
            return await sender.Send(getMembersModel);
        }

        // Search for Members or ids
        // send a post/comment

    }
}
