using Domains.Messaging.GroupMessageEntity;
using Domains.Messaging.GroupMessageEntity.Exceptions;
using Domains.Messaging.GroupMessageEntity.Repos;
using Infra.EFCore.Contexts;
using Infra.EFCore.Exceptions;

namespace Infra.EFCore.Repositories.Messaging.Group.Messages;

internal class GroupMessageCommands(AppDbContext appDbContext) : IGroupMessageCommands {

    [ConcurrencyTryCatch<GroupCommandException>]
    public async Task CreateAsync(GroupMessageTbl message) {
        await appDbContext.GroupMessages.AddAsync(message);
        await appDbContext.SaveChangesAsync();
    }

    [ConcurrencyTryCatch<GroupCommandException>]
    public async Task DeleteAsync(GroupMessageTbl message) {
        appDbContext.GroupMessages.Remove(message);
        await appDbContext.SaveChangesAsync();
    }
    [ConcurrencyTryCatch<GroupCommandException>]
    public async Task UpdateAsync(GroupMessageTbl message) {
        appDbContext.GroupMessages.Update(message);
        await appDbContext.SaveChangesAsync();
    }
}
