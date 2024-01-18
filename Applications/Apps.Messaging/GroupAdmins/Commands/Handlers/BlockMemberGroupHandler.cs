using Apps.Messaging.Exceptions;
using Apps.Messaging.GroupAdmins.Commands.Models;
using Apps.Messaging.Managers;
using Domains.Messaging.GroupMemberEntity.Repos;
using Shared.Abstractions.Messaging.Constants;
using Shared.Models;
namespace Apps.Messaging.GroupAdmins.Commands.Handlers;
internal sealed class BlockMemberGroupHandler(IGroupAdminRepo groupAdminRepo)
    : GroupAdminHandler<BlockMemberModel , Result>(groupAdminRepo) {
    public override async Task<Result> Handle(BlockMemberModel request , CancellationToken cancellationToken) {        
        return await TryToDoActionByAdminAsync(
            request.GroupId ,
            request.AdminId ,
            request.MemberId , 
            async (member,accessLevel) => {
                if(request.AdminId.Equals(request.MemberId)) {
                    throw new GroupAdminsException("BlockMemberAsync" , "NotPossible" , "You can not block yourself");
                }
                if(member.IsAdmin && accessLevel != AdminAccessLevels.Owner) {
                    throw new GroupAdminsException("BlockMemberAsync" , "NotPossible" , "You can not block other admins.");
                }
                await groupAdminRepo.Commands.BlockMemberAsync(member , request.AdminId ,
                 request.StartBlockAt , request.EndBlockAt , request.Reason);
            });      
    }
}
