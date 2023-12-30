using Domains.Messaging.GroupRequesterEntity;
using Domains.Messaging.GroupRequesterEntity.Repos;
using Infra.EFCore.Contexts;
using Microsoft.EntityFrameworkCore;
using Shared.Models;
using Shared.ValueObjects;

namespace Infra.EFCore.Repositories.Messaging;
internal class GroupRequesterRepo(AppDbContext appDbContext) : IGroupRequesterRepo {
    public async Task CreateAsync(GroupRequesterTbl entity) {
        appDbContext.GroupRequesters.Add(entity);
        await appDbContext.SaveChangesAsync();
    }

    public async Task<GroupRequesterTbl?> GetAsync(EntityId groupId , EntityId requesterId) {
        return await appDbContext.GroupRequesters.FirstOrDefaultAsync(x => x.GroupId == groupId && x.RequesterId == requesterId);
    }

    public async Task UpdateAsync(GroupRequesterTbl entity) {
        appDbContext.GroupRequesters.Update(entity);
        await appDbContext.SaveChangesAsync();
    }
}
