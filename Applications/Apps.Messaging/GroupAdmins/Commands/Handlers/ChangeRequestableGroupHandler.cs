using Apps.Messaging.GroupAdmins.Commands.Models;
using Apps.Messaging.Managers;
using Domains.Messaging.GroupMemberEntity.Repos;
using Shared.Models;

namespace Apps.Messaging.GroupAdmins.Commands.Handlers {
    internal sealed class ChangeRequestableGroupHandler(IGroupAdminRepo groupAdminRepo)
        : GroupAdminHandler<GroupRequestableStateModel , Result>(groupAdminRepo) {
        public override async Task<Result> Handle(GroupRequestableStateModel request , CancellationToken cancellationToken) {
            return await TryToDoAsync(request.GroupId , async (findGroup) =>
                   await groupAdminRepo.Commands.ChangeRequestableStateAsync(findGroup , request.IsRequestable));
        }
    }
}
