using Domains.Messaging.GroupRequestEntity;
using Domains.Messaging.GroupRequestEntity.Exceptions;
using Domains.Messaging.GroupRequestEntity.Repos;
using Infra.EFCore.Contexts;
using Infra.EFCore.Exceptions;

namespace Infra.EFCore.Repositories.Messaging.Group.Requests;

internal class GroupRequestCommands(AppDbContext appDbContext) : IGroupRequestCommands {

    [ConcurrencyTryCatch<GroupRequestCommandException>]
    public async Task CreateAsync(GroupRequestTbl groupRequest) {
        await appDbContext.GroupRequests.AddAsync(groupRequest);
        await appDbContext.SaveChangesAsync();
    }

    [ConcurrencyTryCatch<GroupRequestCommandException>]
    public async Task DeleteAsync(GroupRequestTbl groupRequest) {     
        appDbContext.GroupRequests.Remove(groupRequest);
        await appDbContext.SaveChangesAsync();
    }

    [ConcurrencyTryCatch<GroupRequestCommandException>]
    public async Task UpdateAsync(GroupRequestTbl groupRequest) {
        appDbContext.GroupRequests.Update(groupRequest);
        await appDbContext.SaveChangesAsync();
    }
}
