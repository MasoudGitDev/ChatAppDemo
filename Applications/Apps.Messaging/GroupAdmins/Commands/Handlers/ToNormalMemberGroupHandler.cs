using Apps.Messaging.GroupAdmins.Commands.Models;
using Apps.Messaging.GroupAdmins.Shared;
using Domains.Messaging.GroupMemberEntity.Repos;
using MediatR;
using Shared.Abstractions.Messaging.Constants;
using Shared.Models;

namespace Apps.Messaging.GroupAdmins.Commands.Handlers;
internal sealed class ToNormalMemberGroupHandler(IGroupAdminRepo groupAdminRepo)
    : GroupAdminManager(groupAdminRepo), IRequestHandler<ToNormalMemberModel , Result> {
    public async Task<Result> Handle(ToNormalMemberModel request , CancellationToken cancellationToken) {
        return await TryToDoActionByAdminAsync(
         request.GroupId ,
         request.AdminId ,
         request.MemberId ,
         async (member , accessLevel) => {
             if(accessLevel == AdminAccessLevels.High) {
                 await groupAdminRepo.Commands.ToNormalMemberAsync(member);
             }
         });
    }
}
