using Apps.Messaging.GroupAdmins.Commands.Models;
using Apps.Messaging.GroupAdmins.Manager;
using Domains.Messaging.GroupMemberEntity.Repos;
using Shared.Models;

namespace Apps.Messaging.GroupAdmins.Commands.Handlers;
internal sealed class UnblockMemberGroupHandler(IGroupAdminRepo groupAdminRepo)
    : GroupAdminHandler<UnblockMemberModel , Result>(groupAdminRepo) {
    public override async Task<Result> Handle(UnblockMemberModel request , CancellationToken cancellationToken) {
        return await TryToDoActionByAdminAsync(request.GroupId , request.AdminId , request.MemberId ,
           async (member , _) => await groupAdminRepo.Commands.UnblockMemberAsync(member));
    }
}