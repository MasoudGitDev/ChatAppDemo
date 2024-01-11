using Apps.Messaging.GroupAdmins.Commands.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace Server.WebAPI.Controllers.Messaging {
    [Route("api/[controller]")]
    [ApiController]
    public class GroupAdminsController(ISender sender) : ControllerBase {

        [HttpPost("ConfirmRequest")]
        public async Task<Result> ConfirmRequestAsync([FromBody] ConfirmGroupRequestModel model) {
            return await sender.Send(model);
        }

        [HttpPut("ToAdminModel")]
        public async Task<Result> ToAdminModelAsync([FromBody]ToAdminModel model) {
            return await sender.Send(model);
        }

        [HttpPut("ToNormalMemberModel")]
        public async Task<Result> ToNormalMemberModelAsync([FromBody] ToNormalMemberModel model) {
            return await sender.Send(model);
        }  
 
        [HttpPut("ChangeRequestableState")]
        public async Task<Result> ChangeRequestableStateAsync([FromBody] GroupRequestableStateModel model) {
            return await sender.Send(model);
        }

        [HttpPut("Block")]
        public async Task<Result> BlockAsync([FromBody] BlockMemberModel model) {
            return await sender.Send(model);
        }


        [HttpPut("Unblock")]
        public async Task<Result> UnblockAsync([FromBody] UnblockMemberModel model) {
            return await sender.Send(model);
        }

        [HttpPut("AddLogo")]
        public async Task<Result> AddLogoAsync([FromBody] AddLogoModel model) {
            return await sender.Send(model);
        }

        [HttpDelete("DeleteMember")]
        public async Task<Result> DeleteMemberAsync([FromBody] RemoveMemberModel model) {
            return await sender.Send(model);
        }

        [HttpDelete("DeleteGroup")]
        public async Task<Result> DeleteGroupAsync([FromBody] RemoveGroupModel model) {
            return await sender.Send(model);
        }
    }
}
