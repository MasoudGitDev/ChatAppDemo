using Domains.Messaging.GroupEntity.Repo;
using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupMemberEntity;
using Domains.Messaging.GroupRequestEntity;
using Infra.EFCore.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infra.EFCore.Repositories.Messaging.Group;
internal class GroupCommands(AppDbContext appDbContext) : IGroupCommands
{
    public Task SendRequestAsync(GroupRequestTbl requestEntity) {
        throw new NotImplementedException();
    }

    public Task LeaveGroupAsync(GroupMemberTbl groupMemberEntity)
    {
        throw new NotImplementedException();
    }

    public Task RemoveRequestAsync(GroupRequestTbl requestEntity) {
        throw new NotImplementedException();
    }

    public Task UpdateRequestAsync(GroupRequestTbl requestEntity) {
        throw new NotImplementedException();
    }

}
