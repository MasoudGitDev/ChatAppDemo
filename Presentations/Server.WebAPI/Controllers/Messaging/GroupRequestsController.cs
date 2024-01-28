using Apps.Messaging.GroupRequests.Commands.Models;
using Apps.Messaging.GroupRequests.Queries.Models;
using Apps.Messaging.GroupRequests.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Server.WebAPI.Controllers.Shared;
using Shared.Models;

namespace Server.WebAPI.Controllers.Messaging {
    [Route("api/[controller]")]
    [ApiController]
    [ErrorResult]
    public class GroupRequestsController(ISender sender) : ControllerBase {

        [HttpGet("GetGroupRequests")]        
        public async Task<Result<List<GroupRequestResult>>> GetGroupRequestsAsync([FromQuery] GetGroupRequestsModel model) {
            return await sender.Send(model);
        }

        [HttpGet("GetUserRequests")]        
        public async Task<Result<List<GroupRequestResult>>> GetUserRequestsAsync([FromQuery] GetUserRequestsModel model) {
            return await sender.Send(model);
        }

        [HttpPost("Create")]        
        public async Task<Result> CreateAsync([FromBody] CreateRequestModel createModel) {
            return await sender.Send(createModel);
        }

        [HttpPut("Update")]        
        public async Task<Result> UpdateAsync([FromBody] UpdateRequestModel updateModel) {
            return await sender.Send(updateModel);
        }

        [HttpDelete("Remove")]        
        public async Task<Result> RemoveAsync([FromBody] RemoveRequestModel removeModel) {
            return await sender.Send(removeModel);
        }

    }
}
