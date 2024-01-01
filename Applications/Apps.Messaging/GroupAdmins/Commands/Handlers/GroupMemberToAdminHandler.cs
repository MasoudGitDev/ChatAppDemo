using Apps.Messaging.GroupAdmins.Commands.Models;
using Apps.Messaging.GroupAdmins.Shared;
using Domains.Messaging.Shared.UnitOfWorks;
using MediatR;
using Shared.Models;

namespace Apps.Messaging.GroupAdmins.Commands.Handlers;
internal sealed class GroupMemberToAdminHandler(IGroupUnitOfWork groupUnitOfWork)
    : GroupAdminManager(groupUnitOfWork), IRequestHandler<ConvertToAdminModel , Result> {
    public async Task<Result> Handle(ConvertToAdminModel request , CancellationToken cancellationToken) {
        return await TryToDoActionByAdminAsync(
          request.GroupId ,
          request.AdminId ,
          request.MemberId ,
          async (member , accessLevel) => await groupUnitOfWork.AdminRepo.ConvertMemberToAdminAsync(member,
               request.AdminId, accessLevel,request.StartAt,request.EndAt,request.Reason));
    }
}
