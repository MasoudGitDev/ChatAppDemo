using Apps.Messaging.GroupMessages.Queries.Models;
using Apps.Messaging.GroupMessages.Shared;
using Apps.Messaging.Managers;
using Domains.Messaging.GroupMemberEntity.Repos;
using Mapster;
using Shared.Enums;
using Shared.Models;

namespace Apps.Messaging.GroupMessages.Queries.Handlers;
internal class GetGroupMessagesHandler(IGroupAdminRepo groupAdminRepo)
    : GroupMessageHandler<GetGroupMessagesModel , Result<List<MessageResult>>>(groupAdminRepo) {
    public override async Task<Result<List<MessageResult>>> Handle(GetGroupMessagesModel request , CancellationToken cancellationToken) {
        var messages = await GetGroupMessagesAsync(request.GroupId);
        var msgDTOs = messages.Adapt<List<MessageResult>>();
        return new Result<List<MessageResult>>(ResultStatus.Success , null , msgDTOs);
    }
}
