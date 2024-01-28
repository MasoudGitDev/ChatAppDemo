using Apps.Messaging.GroupAdmins.Commands.Models;
using Apps.Messaging.GroupAdmins.Manager;
using Domains.Messaging.GroupMemberEntity.Repos;
using Shared.Models;

namespace Apps.Messaging.GroupAdmins.Commands.Handlers;
internal sealed class DeleteGroupHandler(IGroupAdminRepo groupAdminRepo)
    : GroupAdminHandler<RemoveGroupModel , Result>(groupAdminRepo) {
    public override async Task<Result> Handle(RemoveGroupModel request , CancellationToken cancellationToken) {
        return await DeleteGroupAsync(request.GroupId , request.AdminId);
    }
}
