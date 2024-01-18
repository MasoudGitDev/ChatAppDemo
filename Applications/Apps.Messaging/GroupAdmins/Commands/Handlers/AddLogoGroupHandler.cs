using Apps.Messaging.GroupAdmins.Commands.Models;
using Apps.Messaging.Managers;
using Domains.Messaging.GroupMemberEntity.Repos;
using Shared.Models;

namespace Apps.Messaging.GroupAdmins.Commands.Handlers;
internal sealed class AddLogoGroupHandler(IGroupAdminRepo groupAdminRepo)
    : GroupAdminHandler<AddLogoModel,Result>(groupAdminRepo) {
    public override async Task<Result> Handle(AddLogoModel request , CancellationToken cancellationToken) {
        return await TryToDoAsync(request.GroupId , async (findGroup) => 
               await groupAdminRepo.Commands.AddLogoAsync(findGroup , request.Logo));
    }
}
