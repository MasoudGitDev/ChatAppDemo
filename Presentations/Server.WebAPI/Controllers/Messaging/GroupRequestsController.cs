using Apps.Messaging.GroupRequests.Commands.Models;
using Apps.Messaging.GroupRequests.Queries.Models;
using Apps.Messaging.GroupRequests.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.WebAPI.Controllers.Shared;
using Shared.Enums;
using Shared.Models;

namespace Server.WebAPI.Controllers.Messaging {
    [Route("api/[controller]")]
    [ApiController]
    [ErrorResult]
    [Authorize]
    public class GroupRequestsController(ISender sender) : AuthController {       

        [HttpPost("RequestMembership/{groupId:guid}")]
        public async Task<Result> RequestMembershipAsync([FromRoute] Guid groupId) {
            return await sender.Send(new RequestMembershipModel { GroupId = groupId , UserId = GetUserId() });
        }

        [HttpDelete("Remove/{groupId:guid}")]
        public async Task<Result> RemoveAsync([FromRoute] Guid groupId) {
            return await sender.Send(new RemoveRequestModel { GroupId = groupId , RequesterId = GetUserId() });
        }

        [HttpGet("GetGroupRequests/{groupId:guid}")]
        public async Task<Result<List<GroupRequestResult>>> GetGroupRequestsAsync([FromRoute] Guid groupId) {
            return await sender.Send(new GetGroupRequestsModel { GroupId = groupId , IsVisible = Visibility.Visible });
        }

        [HttpGet("GetHiddenGroupRequests/{groupId:guid}")]
        public async Task<Result<List<GroupRequestResult>>> GetHiddenGroupRequestsAsync([FromRoute] Guid groupId) {
            return await sender.Send(new GetGroupRequestsModel { GroupId = groupId , IsVisible = Visibility.Hidden });
        }

        [HttpGet("GetUserRequests/{requesterId:guid}")]
        public async Task<Result<List<GroupRequestResult>>> GetUserRequestsAsync([FromRoute] Guid requesterId) {
            return await sender.Send(new GetUserRequestsModel { RequesterId = requesterId , Visibility = Visibility.Visible });
        }

        [HttpGet("GetHiddenUserRequests/{requesterId:guid}")]
        public async Task<Result<List<GroupRequestResult>>> GetHiddenUserRequestsAsync([FromRoute] Guid requesterId) {
            return await sender.Send(new GetUserRequestsModel { RequesterId = requesterId , Visibility = Visibility.Hidden });
        }

    }
}
