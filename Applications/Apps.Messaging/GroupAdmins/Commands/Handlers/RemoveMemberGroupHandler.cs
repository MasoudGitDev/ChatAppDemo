using Apps.Messaging.GroupAdmins.Commands.Models;
using Apps.Messaging.Managers;
using Domains.Messaging.GroupMemberEntity.Repos;
using Shared.Abstractions.Messaging.Constants;
using Shared.Models;
namespace Apps.Messaging.GroupAdmins.Commands.Handlers {
    internal sealed class RemoveMemberGroupHandler(IGroupAdminRepo groupAdminRepo)
        : GroupAdminHandler<RemoveMemberModel , Result>(groupAdminRepo) {
        public override async Task<Result> Handle(RemoveMemberModel request , CancellationToken cancellationToken) {
            return await TryToDoActionByAdminAsync(request.GroupId , request.AdminId , request.MemberId ,
            async (member , accessLevel) => {
                if(accessLevel != AdminAccessLevels.Low) {
                    await groupAdminRepo.Commands.DeleteMemberAsync(member);
                }
            });
        }
    }
}
