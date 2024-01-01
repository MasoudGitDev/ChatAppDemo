using Apps.Messaging.GroupAdmins.Commands.Models;
using Apps.Messaging.GroupAdmins.Shared;
using Domains.Messaging.Shared.UnitOfWorks;
using MediatR;
using Shared.Models;

namespace Apps.Messaging.GroupAdmins.Commands.Handlers;
internal sealed class GroupAdminToMemberHandler(IGroupUnitOfWork groupUnitOfWork)
    : GroupAdminManager(groupUnitOfWork), IRequestHandler<RegularMemberModel , Result> {
    public async Task<Result> Handle(RegularMemberModel request , CancellationToken cancellationToken) {
        return await TryToDoActionByAdminAsync(
         request.GroupId ,
         request.AdminId ,
         request.MemberId ,
         groupUnitOfWork.AdminRepo.ConvertAdminToMemberAsync);
    }
}
