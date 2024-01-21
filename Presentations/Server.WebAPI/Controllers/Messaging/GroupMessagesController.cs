using Apps.Messaging.GroupMessages.Commands.Models;
using Apps.Messaging.GroupMessages.Queries.Models;
using Apps.Messaging.GroupMessages.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Server.WebAPI.Controllers.Shared;
using Shared.Models;

namespace Server.WebAPI.Controllers.Messaging {
    [Route("api/[controller]")]
    [ApiController]
    [ErrorResult]
    public class GroupMessagesController(ISender sender) : ControllerBase {

        [HttpGet("GetGroupMessages")]
        public async Task<Result<List<MessageResult>>> GetGroupMessagesAsync([FromQuery] GetGroupMessagesModel model)
            => await sender.Send(model);

        [HttpPost("Send")]
        public async Task<Result> SendAsync([FromBody]CreateMessageModel model) => await sender.Send(model);

        [HttpPut("Update")]
        public async Task<Result> UpdateAsync([FromBody]UpdateMessageModel model) => await sender.Send(model);

        [HttpDelete("Remove")]
        public async Task<Result> RemoveAsync([FromBody] RemoveMessageModel model) => await sender.Send(model);

    }
}
