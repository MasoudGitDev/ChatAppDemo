using Apps.Messaging.Groups.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.WebAPI.Controllers.Shared;
using Shared.Models;

namespace Server.WebAPI.Controllers.Messaging {
    [Route("api/[controller]")]
    [ApiController]
    [ErrorResult]
    public class GroupMessagesController(ISender sender) : ControllerBase {

       
    }
}
