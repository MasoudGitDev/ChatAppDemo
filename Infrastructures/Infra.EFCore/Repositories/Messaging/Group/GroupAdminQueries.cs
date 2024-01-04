using Domains.Messaging.GroupMemberEntity.Repos;
using Domains.Messaging.GroupRequestEntity;
using Domains.Messaging.Shared.Models;
using Domains.Messaging.Shared.ValueObjects;
using Infra.EFCore.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Shared.ValueObjects;

namespace Infra.EFCore.Repositories.Messaging.Group;
internal class GroupAdminQueries(AppDbContext appDbContext) : IGroupAdminQueries
{
    public async Task<List<AdminMemberResult>> GetAdminsAsync(EntityId groupId)
    {
        return await appDbContext.GroupMembers.AsNoTracking()
          .Where(x => x.GroupId == groupId).Where(x => x.IsAdmin == true)
          .Select(x => new AdminMemberResult(x.MemberId, x.AdminInfo!)).ToListAsync();
    }

    public async Task<List<BlockMemberResult>> GetBlockedMembersAsync(EntityId groupId)
    {
        return await appDbContext.GroupMembers.AsNoTracking()
        .Where(x => x.GroupId == groupId).Where(x => x.IsBlocked == true)
        .Select(x => new BlockMemberResult(x.MemberId, x.BlockMemberInfo!)).ToListAsync();
    }

    public async Task<List<GroupRequestTbl>> GetRequestsAsync(EntityId groupId)
    {
        return await appDbContext.GroupRequests.AsNoTracking()
           .Where(x => x.GroupId == groupId)
           .ToListAsync();
    }

    public async Task<AdminMemberInfo?> IsAdminAsync(EntityId groupId, EntityId memberId)
    {
        return await appDbContext.GroupMembers.AsNoTracking()
          .Where(x => x.GroupId == groupId).Where(x => x.MemberId == memberId)
          .Select(x => x.AdminInfo).SingleOrDefaultAsync();
    }
}
