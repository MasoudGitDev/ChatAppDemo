using Apps.Messaging.GroupMembers.Commands.Models;
using Apps.Messaging.GroupMembers.Queries.Models;
using Domains.Messaging.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace Server.WebAPI.Controllers.Messaging {
    [Route("api/[controller]")]
    [ApiController]
    public class GroupMembersController(ISender sender) : ControllerBase {

       [HttpGet("Members")]
       public async Task<Result<List<MemberInfo>>> GetMembers([FromQuery]GetMembersModel members)
            => await sender.Send(members);


        [HttpGet("Groups")]
        public async Task<Result<List<GroupInfo>>> GetGroups([FromQuery] GetGroupsModel groups)
          => await sender.Send(groups);

        [HttpDelete("RemoveMember")]
        public async Task<Result> RemoveMemberFromGroupAsync([FromQuery] RemoveGroupMemberModel member)
            => await sender.Send(member);

    }
}
