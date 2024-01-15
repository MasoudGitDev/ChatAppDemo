using Domains.Messaging.GroupEntity;
using Domains.Messaging.GroupEntity.Repo;
using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupMemberEntity;
using Domains.Messaging.Shared.ValueObjects;
using Infra.EFCore.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infra.EFCore.Repositories.Messaging.Group;
internal class GroupQueries(AppDbContext appDbContext) : IGroupQueries
{
    public async Task<List<GroupMemberTbl>> GetMembersAsync(GroupId groupId)
    {
        return await appDbContext.GroupMembers.AsNoTracking().Where(x=> x.GroupId ==groupId).ToListAsync();
    }
    public async Task<GroupTbl?> GetGroupAsync(GroupId groupId)
    {
        return await appDbContext.Groups.AsNoTracking().Where(x => x.GroupId ==groupId).FirstOrDefaultAsync();
    }

    public async Task<GroupMemberTbl?> GetMemberAsync(GroupId groupId, AppUserId memberId)
    {
        return await appDbContext.GroupMembers.AsNoTracking().Where(x => x.GroupId==groupId && x.MemberId ==memberId).FirstOrDefaultAsync();
    }

    public async Task<GroupTbl?> GetGroupByDisplayIdAsync(DisplayId displayId) {
        return await appDbContext.Groups.AsNoTracking().Where(x => x.DisplayId ==displayId).FirstOrDefaultAsync();
    }

    public async Task<GroupId?> GetIdByDisplayIdAsync(DisplayId displayId) {
        return await appDbContext.Groups.AsNoTracking().Where(x=>x.DisplayId ==displayId).Select(x=>x.GroupId).FirstOrDefaultAsync();
    }

    public async Task<List<GroupMemberTbl>> GetMembersByDisplayIdAsync(DisplayId displayId) {
        return await appDbContext.GroupMembers.AsNoTracking().Include(x=>x.Group)
                    .Where(x=>x.Group.DisplayId ==displayId && !x.IsBlocked).ToListAsync();
    }
}
