using Apps.Messaging.GroupRequesters.Commands.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace Server.WebAPI.Controllers.Messaging {
    [Route("api/[controller]")]
    [ApiController]
    public class GroupRequestersController(ISender sender) : ControllerBase {

        [HttpPost("Create")]
        public async Task<Result> Create([FromBody] CreateGRModel createModel) {
            return await sender.Send(createModel);
        }
    }
}
