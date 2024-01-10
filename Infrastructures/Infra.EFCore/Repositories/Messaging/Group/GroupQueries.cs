using Domains.Messaging.GroupEntity;
using Domains.Messaging.GroupEntity.Repo;
using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupMemberEntity;
using Domains.Messaging.GroupRequestEntity;
using Domains.Messaging.Shared.ValueObjects;
using Infra.EFCore.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infra.EFCore.Repositories.Messaging.Group;
internal class GroupQueries(AppDbContext appDbContext) : IGroupQueries
{
    public async Task<List<GroupMemberTbl>> GetMembersAsync(GroupId groupId)
    {
        return await appDbContext.GroupMembers.AsNoTracking().ToListAsync();
    }

    public async Task<GroupRequestTbl?> GetRequestAsync(GroupId groupId, AppUserId requestId)
    {
        return await appDbContext.GroupRequests.AsNoTracking()
           .Where(x => x.GroupId == groupId).Where(x => x.RequesterId == requestId).FirstOrDefaultAsync();
    }

    public async Task<GroupTbl?> GetGroupAsync(GroupId groupId)
    {
        return await appDbContext.Groups.AsNoTracking().Where(x => x.GroupId == groupId).FirstOrDefaultAsync();
    }

    public async Task<GroupMemberTbl?> GetMemberAsync(GroupId groupId, AppUserId memberId)
    {
        return await appDbContext.GroupMembers.AsNoTracking().Where(x => x.MemberId == memberId)
            .Where(x => x.GroupId == groupId).FirstOrDefaultAsync();
    }

    public async Task<GroupTbl?> GetGroupByDisplayIdAsync(DisplayId displayId) {
        return await appDbContext.Groups.AsNoTracking().Where(x => x.DisplayId == displayId).FirstOrDefaultAsync();
    }

    public async Task<GroupId?> GetIdByDisplayIdAsync(DisplayId displayId) {
        return await appDbContext.Groups.Where(x=>x.DisplayId.Equals(displayId)).Select(x=>x.GroupId).FirstOrDefaultAsync();
    }

    public async Task<List<GroupMemberTbl>> GetMembersByDisplayIdAsync(DisplayId displayId) {
        return await appDbContext.GroupMembers.Include(x=>x.Group).Where(x=>x.Group.DisplayId == displayId).ToListAsync();
    }
}
