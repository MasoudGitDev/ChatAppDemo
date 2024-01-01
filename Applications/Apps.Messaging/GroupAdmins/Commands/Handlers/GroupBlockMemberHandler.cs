using Apps.Messaging.GroupAdmins.Commands.Models;
using Apps.Messaging.GroupAdmins.Shared;
using Domains.Messaging.Shared.UnitOfWorks;
using MediatR;
using Shared.Models;
namespace Apps.Messaging.GroupAdmins.Commands.Handlers;
internal sealed class GroupBlockMemberHandler (IGroupUnitOfWork groupUnitOfWork)
    : GroupAdminManager(groupUnitOfWork) , IRequestHandler<BlockMemberModel , Result> {
    public async Task<Result> Handle(BlockMemberModel request , CancellationToken cancellationToken) {        
        return await TryToDoActionByAdminAsync(
            request.GroupId ,
            request.AdminId ,
            request.MemberId , 
            async (member,_) => await groupUnitOfWork.AdminRepo.BlockAsync(member, request.AdminId ,
                 request.StartBlockAt , request.EndBlockAt , request.Reason));      
    }
}
