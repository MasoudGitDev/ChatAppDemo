using Apps.Messaging.GroupAdmins.Commands.Models;
using Apps.Messaging.GroupAdmins.Shared;
using Domains.Messaging.GroupMemberEntity.Repos;
using MediatR;
using Shared.Abstractions.Messaging.Constants;
using Shared.Enums;
using Shared.Models;
namespace Apps.Messaging.GroupAdmins.Commands.Handlers {
    internal sealed class RemoveMemberGroupHandler(IGroupAdminRepo groupAdminRepo)
        : GroupAdminManager(groupAdminRepo), IRequestHandler<RemoveMemberModel , Result> {
        public async Task<Result> Handle(RemoveMemberModel request , CancellationToken cancellationToken) {
            return await TryToDoActionByAdminAsync(
                   request.GroupId ,
                   request.AdminId ,
                   request.MemberId ,
                   async (member,accessLevel) => {
                       if(accessLevel != AdminAccessLevels.Low) {
                           await groupAdminRepo.Commands.DeleteMemberAsync(member);
                       }                       
                   });
        }
    }
}
