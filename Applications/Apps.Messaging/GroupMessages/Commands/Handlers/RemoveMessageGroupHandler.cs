using Apps.Messaging.GroupMessages.Commands.Models;
using Apps.Messaging.GroupMessages.Manager;
using Domains.Messaging.GroupMemberEntity.Repos;
using Shared.Models;

namespace Apps.Messaging.GroupMessages.Commands.Handlers;
internal sealed class RemoveMessageGroupHandler(IGroupAdminRepo groupAdminRepo)
    : GroupMessageHandler<RemoveMessageModel , Result>(groupAdminRepo) {
    public override async Task<Result> Handle(RemoveMessageModel request , CancellationToken cancellationToken) {
        return await RemoveAsync(request);
    }
}
