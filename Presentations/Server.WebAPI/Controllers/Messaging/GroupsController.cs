using Apps.Messaging.Groups.Commands.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace Server.WebAPI.Controllers.Messaging
{
    [Route("Api/Messaging/[controller]")]
    [ApiController]
    public class GroupsController(ISender sender) : ControllerBase {
        [HttpPost("Create")]
        public async Task<Result> CreateAsync([FromBody]CreateCModel createModel) {
            return await sender.Send(createModel);
        }
    }
}
