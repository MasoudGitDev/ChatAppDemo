using Apps.Messaging.Exceptions;
using Apps.Messaging.GroupAdmins.Commands.Models;
using Apps.Messaging.Managers;
using Domains.Messaging.GroupMemberEntity.Repos;
using Shared.Abstractions.Messaging.Constants;
using Shared.Models;

namespace Apps.Messaging.GroupAdmins.Commands.Handlers;
internal sealed class ToNormalMemberGroupHandler(IGroupAdminRepo groupAdminRepo)
    : GroupAdminHandler<ToNormalMemberModel,Result>(groupAdminRepo) {
    public override async Task<Result> Handle(ToNormalMemberModel request , CancellationToken cancellationToken) {
        return await TryToDoActionByAdminAsync( request.GroupId , request.AdminId , request.MemberId ,
         async (member , accessLevel) => {
             if(request.AdminId.Equals(request.MemberId) && accessLevel == AdminAccessLevels.Owner) {
                 throw new GroupAdminsException("ToNormalMember" , "NotPossible" , "If owner wants to change him/her admin lever must use another action!");
             }
             if(request.AdminId.Equals(request.MemberId) && accessLevel != AdminAccessLevels.Owner) {
                 await groupAdminRepo.Commands.ToNormalMemberAsync(member);
                 return;
             }
             if(accessLevel != AdminAccessLevels.Owner) {
                 throw new GroupAdminsException("ToNormalMember" , "NotAccess" , "Just owner can do it.");
             }
             await groupAdminRepo.Commands.ToNormalMemberAsync(member);
         });
    }
}
