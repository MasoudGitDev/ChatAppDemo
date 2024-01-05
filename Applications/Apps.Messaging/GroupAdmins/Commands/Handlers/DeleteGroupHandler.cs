using Apps.Messaging.GroupAdmins.Commands.Models;
using Apps.Messaging.GroupAdmins.Shared;
using Domains.Messaging.GroupMemberEntity.Repos;
using MediatR;
using Shared.Models;

namespace Apps.Messaging.GroupAdmins.Commands.Handlers;
internal sealed class DeleteGroupHandler(IGroupAdminRepo groupAdminRepo)
    : GroupAdminManager(groupAdminRepo), IRequestHandler<RemoveGroupModel , Result> {
    public async Task<Result> Handle(RemoveGroupModel request , CancellationToken cancellationToken) {
        return await DeleteGroupAsync(request.GroupId , request.AdminId);
    }
}
