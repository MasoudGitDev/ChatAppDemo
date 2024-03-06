using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupRequestEntity;
using Domains.Messaging.GroupRequestEntity.Repos;
using Domains.Messaging.Shared.ValueObjects;
using Infra.EFCore.Contexts;
using Microsoft.EntityFrameworkCore;
using Shared.Enums;

namespace Infra.EFCore.Repositories.Messaging.GroupRequests;

internal class GroupRequestQueries(AppDbContext appDbContext) : IGroupRequestQueries
{
    public async Task<List<GroupRequestTbl>> GetGroupRequestsAsync(
        GroupId groupId ,
        Visibility visible = Visibility.Visible)
    {
        return await appDbContext.GroupRequests
            .AsNoTracking()
            .Where(x => x.GroupId == groupId && x.Visibility.showToAdmins == visible)            
            .ToListAsync();
    }

    public async Task<List<GroupRequestTbl>> GetUserRequestsAsync(
        AppUserId appUserId ,
        Visibility visible = Visibility.Visible)
    {
        return await appDbContext.GroupRequests
            .AsNoTracking()
            .Where(x => x.RequesterId == appUserId && x.Visibility.showToRequester == visible)
            .ToListAsync();
    }
    public async Task<GroupRequestTbl?> GetRequestAsync(GroupId groupId , AppUserId appUserId) {
        return await appDbContext.GroupRequests
            .AsNoTracking()
            .Where(x => x.GroupId == groupId && x.RequesterId == appUserId).FirstOrDefaultAsync();
    }
}
