using Apps.Messaging.GroupAdmins.Shared;
using Apps.Messaging.GroupMembers.Commands.Models;
using Domains.Messaging.Shared.UnitOfWorks;
using MediatR;
using Shared.Models;
namespace Apps.Messaging.GroupAdmins.Commands.Handlers {
    internal sealed class GroupRemoveMemberByAdminHandler(IGroupUnitOfWork groupUnitOfWork)
        : GroupAdminManager(groupUnitOfWork), IRequestHandler<RemoveGroupMemberModel , Result> {
        public async Task<Result> Handle(RemoveGroupMemberModel request , CancellationToken cancellationToken) {
            return await TryToDoActionByAdminAsync(
                   request.GroupId ,
                   request.AdminId ,
                   request.MemberId ,
                   groupUnitOfWork.AdminRepo.DeleteMemberAsync);
        }
    }
}
