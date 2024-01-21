using Apps.Messaging.GroupMessages.Commands.Models;
using Apps.Messaging.Managers;
using Domains.Messaging.GroupMemberEntity.Repos;
using Shared.Models;

namespace Apps.Messaging.GroupMessages.Commands.Handlers;
internal sealed class CreateMessageGroupHandler(IGroupAdminRepo groupAdminRepo)
    : GroupMessageHandler<CreateMessageModel , Result>(groupAdminRepo) {
    public override async Task<Result> Handle(CreateMessageModel request , CancellationToken cancellationToken) {
        return await CreateAsync(request);
    }
}
