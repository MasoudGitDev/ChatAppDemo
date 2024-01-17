using Apps.Messaging.GroupAdmins.Commands.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Server.WebAPI.Controllers.Shared;
using Shared.Models;
namespace Server.WebAPI.Controllers.Messaging {
    [Route("api/[controller]")]
    [ApiController]
    public class GroupAdminsController(ISender sender) : BaseAPIController {

        [HttpPost("ConfirmRequest")]
        public async Task<Result> ConfirmRequestAsync([FromBody] ConfirmGroupRequestModel model) {
            return await sender.Send(model);
        }

        [HttpPut("ToAdminMember")]
        public async Task<Result> ToAdminModelAsync([FromBody] ToAdminMemberModel model) {
            return await sender.Send(model);
        }

        [HttpPut("ToNormalMember")]
        public async Task<Result> ToNormalMemberModelAsync([FromBody] ToNormalMemberModel model) {
            return await sender.Send(model);
        }

        [HttpPut("ChangeRequestableState")]
        public async Task<Result> ChangeRequestableStateAsync([FromBody] GroupRequestableStateModel model) {
            return await sender.Send(model);
        }

        [HttpPut("BlockMember")]
        public async Task<Result> BlockAsync([FromBody] BlockMemberModel model) {
            return await MethodResultAsync(async () => await sender.Send(model));
        }


        [HttpPut("UnblockMember")]
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

        [HttpGet("GetBlockedMembers")]
        public async Task<Result> GetBlockedMembersAsync([FromQuery] BlockMemberModel model)
            => await sender.Send(model);
    }
}
