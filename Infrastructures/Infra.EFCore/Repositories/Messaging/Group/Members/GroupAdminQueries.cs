using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupMemberEntity.Repos;
using Domains.Messaging.GroupRequestEntity;
using Domains.Messaging.GroupRequestEntity.ValueObjects;
using Domains.Messaging.Shared.Models;
using Domains.Messaging.Shared.ValueObjects;
using Infra.EFCore.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Shared.ValueObjects;

namespace Infra.EFCore.Repositories.Messaging.Group.Members;
internal class GroupAdminQueries(AppDbContext appDbContext) : IGroupAdminQueries
{
    public async Task<List<AdminMemberResult>> GetAdminsAsync(GroupId groupId)
    {
        return await appDbContext.GroupMembers.AsNoTracking()
          .Where(x => x.GroupId == groupId).Where(x => x.IsAdmin == true)
          .Select(x => new AdminMemberResult(x.MemberId, x.AdminInfo!)).ToListAsync();
    }

    public async Task<List<BlockMemberResult>> GetBlockedMembersAsync(GroupId groupId)
    {
        return await appDbContext.GroupMembers.AsNoTracking()
        .Where(x => x.GroupId == groupId).Where(x => x.IsBlocked == true)
        .Select(x => new BlockMemberResult(x.MemberId, x.BlockMemberInfo!)).ToListAsync();
    }

    public async Task<List<GroupRequestTbl>> GetRequestsAsync(GroupRequestId requestId)
    {
        return await appDbContext.GroupRequests.AsNoTracking()
           .Where(x => x.GroupId == requestId)
           .ToListAsync();
    }

    public async Task<AdminMemberInfo?> GetAdminMemberAsync(GroupId groupId, AppUserId memberId)
    {
        return await appDbContext.GroupMembers.AsNoTracking()
          .Where(x => x.GroupId == groupId).Where(x => x.MemberId == memberId)
          .Select(x => x.AdminInfo).SingleOrDefaultAsync();
    }
}
