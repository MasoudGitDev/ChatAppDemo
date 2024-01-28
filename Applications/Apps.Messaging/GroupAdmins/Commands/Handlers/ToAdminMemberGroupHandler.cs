using Apps.Messaging.Exceptions;
using Apps.Messaging.GroupAdmins.Commands.Models;
using Apps.Messaging.GroupAdmins.Manager;
using Domains.Messaging.GroupMemberEntity.Repos;
using Shared.Abstractions.Messaging.Constants;
using Shared.Models;

namespace Apps.Messaging.GroupAdmins.Commands.Handlers;
internal sealed class ToAdminMemberGroupHandler(IGroupAdminRepo groupAdminRepo)
    : GroupAdminHandler<ToAdminMemberModel , Result>(groupAdminRepo) {
    public override async Task<Result> Handle(ToAdminMemberModel request , CancellationToken cancellationToken) {
        return await TryToDoActionByAdminAsync( request.GroupId ,request.AdminId , request.MemberId ,
          async (member , accessLevel) => {
              if(request.AdminId.Equals(request.MemberId)) {
                  throw new GroupAdminsException("ToAdminMember" , "NotPossible" , "You can not change your admin level.");
              }
              if(accessLevel != AdminAccessLevels.Owner) {
                  throw new GroupAdminsException("ToAdminMember" , "NotAccess" , "Just High or creator can use this command.");
                  
              }
              AdminAccessLevels levelToAssign = request.LevelToAssign ?? AdminAccessLevels.Low;
              if(levelToAssign == AdminAccessLevels.Owner) {
                  throw new GroupAdminsException("ToAdminMember" , "NotPossible" , "Each group can have one owner!");
              }
              await groupAdminRepo.Commands.ToAdminMemberAsync(member , request.AdminId ,levelToAssign ,
                        request.StartAt , request.EndAt , request.Reason);
          });
    }
}
