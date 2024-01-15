using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupMemberEntity;
using Domains.Messaging.GroupMemberEntity.Repos;
using Domains.Messaging.Shared.Models;
using Domains.Messaging.Shared.ValueObjects;
using Infra.EFCore.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infra.EFCore.Repositories.Messaging.Group.Members;
internal class GroupAdminQueries(AppDbContext appDbContext) : IGroupAdminQueries
{
    public async Task<List<AdminMemberResult>> GetAdminsAsync(GroupId groupId)
    {
        return await appDbContext.GroupMembers.AsNoTracking().Where(x => x.GroupId ==groupId && x.IsAdmin)
          .Select(x => new AdminMemberResult(x.MemberId, x.AdminInfo!)).ToListAsync();
    }

    public async Task<List<BlockMemberResult>> GetBlockedMembersAsync(GroupId groupId)
    {
        return await appDbContext.GroupMembers.AsNoTracking().Where(x => x.GroupId == groupId && x.IsBlocked == true)
        .Select(x => new BlockMemberResult(x.MemberId, x.BlockMemberInfo!)).ToListAsync();
    }
    public async Task<AdminMemberInfo?> GetAdminMemberAsync(GroupId groupId, AppUserId memberId)
    {
        return await appDbContext.GroupMembers.AsNoTracking()
          .Where(x => x.GroupId ==groupId && x.MemberId == memberId && x.IsAdmin)
          .Select(x => x.AdminInfo).FirstOrDefaultAsync();
    }
}
