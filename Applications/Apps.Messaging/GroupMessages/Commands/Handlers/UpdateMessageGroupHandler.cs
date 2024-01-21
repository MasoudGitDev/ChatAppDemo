using Apps.Messaging.GroupMessages.Commands.Models;
using Apps.Messaging.Managers;
using Domains.Messaging.GroupMemberEntity.Repos;
using Shared.Enums;
using Shared.Models;

namespace Apps.Messaging.GroupMessages.Commands.Handlers;
internal sealed class UpdateMessageGroupHandler(IGroupAdminRepo groupAdminRepo)
    : GroupMessageHandler<UpdateMessageModel , Result>(groupAdminRepo) {
    public override async Task<Result> Handle(UpdateMessageModel request , CancellationToken cancellationToken) {
        return await UpdateAsync(request);
    }
}
