using Apps.Messaging.GroupRequesters.Commands.Models;
using Apps.Messaging.GroupRequesters.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace Server.WebAPI.Controllers.Messaging {
    [Route("api/[controller]")]
    [ApiController]
    public class GroupRequestersController(ISender sender) : ControllerBase {

        [HttpGet("Get")]
        public async Task<Result<string>> GetAsync([FromBody] GetGroupRequesterModel getModel) {
            return await sender.Send(getModel);
        }

        [HttpPost("Create")]
        public async Task<Result> CreateAsync([FromBody] CreateGroupRequesterModel createModel) {
            return await sender.Send(createModel);
        }

        [HttpPut("Update")]
        public async Task<Result> UpdateAsync([FromBody] UpdateGroupRequesterModel updateModel) {
            return await sender.Send(updateModel);
        }

        [HttpDelete("Remove")]
        public async Task<Result> RemoveAsync([FromBody] RemoveGroupRequesterModel removeModel) {
            return await sender.Send(removeModel);
        }

    }
}
