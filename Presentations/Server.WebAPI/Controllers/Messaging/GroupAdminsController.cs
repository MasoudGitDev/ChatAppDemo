using Apps.Messaging.GroupAdmins.Commands.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Server.WebAPI.Controllers.Shared;
using Shared.Models;
namespace Server.WebAPI.Controllers.Messaging {
    [Route("api/[controller]")]
    [ApiController]
    [ErrorResult]
    public class GroupAdminsController(ISender sender) : ControllerBase {

        [HttpPost("ConfirmRequest")]
        public async Task<Result> ConfirmRequestAsync([FromBody] ConfirmGroupRequestModel model) {
            return await sender.Send(model);
        }

        [HttpPut("ToAdminMember")]
        public async Task<Result> ToAdminMemberAsync([FromBody] ToAdminMemberModel model) {
            return await sender.Send(model);
        }

        [HttpPut("ToNormalMember")]
        public async Task<Result> ToNormalMemberAsync([FromBody] ToNormalMemberModel model) {
            return await sender.Send(model);
        }

        [HttpPut("ChangeRequestableState")]
        public async Task<Result> ChangeRequestableStateAsync([FromBody] GroupRequestableStateModel model) {
            return await sender.Send(model);
        }

        [HttpPut("BlockMember")]        
        public async Task<Result> BlockMemberAsync([FromBody] BlockMemberModel model) {
            return await sender.Send(model);
        }


        [HttpPut("UnblockMember")]
        public async Task<Result> UnblockMemberAsync([FromBody] UnblockMemberModel model) {
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

        [HttpGet("GetBlockedMembers")]
        public async Task<Result> GetBlockedMembersAsync([FromQuery] BlockMemberModel model)
            => await sender.Send(model);
    }
}
