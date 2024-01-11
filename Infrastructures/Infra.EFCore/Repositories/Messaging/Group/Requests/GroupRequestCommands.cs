using Domains.Messaging.GroupRequestEntity;
using Domains.Messaging.GroupRequestEntity.Repos;
using Infra.EFCore.Contexts;
using Infra.EFCore.Repositories.Messaging.Exceptions;


namespace Infra.EFCore.Repositories.Messaging.Group.Requests;

internal class GroupRequestCommands(AppDbContext appDbContext) : IGroupRequestCommands {

    [TryCatch]
    public async Task CreateAsync(GroupRequestTbl groupRequest) {
        await appDbContext.GroupRequests.AddAsync(groupRequest);
        await appDbContext.SaveChangesAsync();
    }

    [TryCatch]
    public async Task DeleteAsync(GroupRequestTbl groupRequest) {     
        appDbContext.GroupRequests.Remove(groupRequest);
        await appDbContext.SaveChangesAsync();
    }

    [TryCatch]
    public async Task UpdateAsync(GroupRequestTbl groupRequest) {
        appDbContext.GroupRequests.Update(groupRequest);
        await appDbContext.SaveChangesAsync();
    }
}
