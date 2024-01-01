using Apps.Messaging.GroupAdmins.Commands.Models;
using Apps.Messaging.GroupAdmins.Shared;
using Domains.Messaging.Shared.UnitOfWorks;
using MediatR;
using Shared.Models;

namespace Apps.Messaging.GroupAdmins.Commands.Handlers;
internal sealed class GroupFreeMemberHandler(IGroupUnitOfWork groupUnitOfWork) 
    :GroupAdminManager(groupUnitOfWork) , IRequestHandler<FreeMemberModel , Result> {
    public async Task<Result> Handle(FreeMemberModel request , CancellationToken cancellationToken) {  
        return await TryToDoActionByAdminAsync(
           request.GroupId ,
           request.AdminId ,
           request.MemberId ,
           async (member,_) => await groupUnitOfWork.AdminRepo.FreeAsync(member));
    }
}
