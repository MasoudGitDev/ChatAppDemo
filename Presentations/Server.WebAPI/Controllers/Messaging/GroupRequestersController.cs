using Apps.Messaging.GroupRequests.Commands.Models;
using Apps.Messaging.GroupRequests.Queries.Models;
using Apps.Messaging.GroupRequests.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace Server.WebAPI.Controllers.Messaging {
    [Route("api/[controller]")]
    [ApiController]
    public class GroupRequestersController(ISender sender) : ControllerBase {

        [HttpGet("Get")]
        public async Task<Result<GroupRequesterResult>> GetAsync([FromQuery] GetGroupRequestModel getModel) {
            return await sender.Send(getModel);
        }

        [HttpPost("Create")]
        public async Task<Result> CreateAsync([FromBody] CreateGroupRequestModel createModel) {
            return await sender.Send(createModel);
        }

        [HttpPut("Update")]
        public async Task<Result> UpdateAsync([FromBody] UpdateGroupRequestModel updateModel) {
            return await sender.Send(updateModel);
        }

        [HttpDelete("Remove")]
        public async Task<Result> RemoveAsync([FromBody] RemoveGroupRequestModel removeModel) {
            return await sender.Send(removeModel);
        }

    }
}
