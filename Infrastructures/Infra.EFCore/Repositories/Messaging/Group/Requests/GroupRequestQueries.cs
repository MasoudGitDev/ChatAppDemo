using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupRequestEntity;
using Domains.Messaging.GroupRequestEntity.Repos;
using Domains.Messaging.Shared.ValueObjects;
using Infra.EFCore.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infra.EFCore.Repositories.Messaging.Group.Requests;

internal class GroupRequestQueries(AppDbContext appDbContext) : IGroupRequestQueries {
    public async Task<List<GroupRequestTbl>> GetGroupRequestsAsync(GroupId groupId) {
        return await appDbContext.GroupRequests.Where(x=>x.GroupId ==groupId).ToListAsync();
    }

    public async Task<GroupRequestTbl?> GetRequestAsync(GroupId groupId , AppUserId appUserId) {
        return await appDbContext.GroupRequests
            .Where(x => x.GroupId == groupId && x.RequesterId ==appUserId).FirstOrDefaultAsync();
    }

    public async Task<List<GroupRequestTbl>> GetUserRequestsAsync(AppUserId appUserId) {
        return await appDbContext.GroupRequests.Where(x => x.RequesterId ==appUserId).ToListAsync();
    }
}
