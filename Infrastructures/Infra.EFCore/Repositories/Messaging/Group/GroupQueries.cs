using Domains.Messaging.GroupEntity;
using Domains.Messaging.GroupEntity.Repo;
using Domains.Messaging.GroupMemberEntity;
using Domains.Messaging.GroupRequestEntity;
using Infra.EFCore.Contexts;
using Microsoft.EntityFrameworkCore;
using Shared.ValueObjects;
namespace Infra.EFCore.Repositories.Messaging.Group;
internal class GroupQueries(AppDbContext appDbContext) : IGroupQueries
{
    public async Task<List<GroupMemberTbl>> GetMembersAsync(EntityId groupId)
    {
        return await appDbContext.GroupMembers
             .AsNoTracking()
             .Where(x => x.GroupId == groupId)
             .ToListAsync();
    }

    public async Task<GroupRequestTbl?> GetRequestAsync(EntityId groupId, EntityId requesterId)
    {
        return await appDbContext.GroupRequests.AsNoTracking()
           .Where(x => x.GroupId == groupId).Where(x => x.RequesterId == requesterId)
           .FirstOrDefaultAsync();
    }

    public async Task<GroupTbl?> GetGroupAsync(EntityId groupId)
    {
        return await appDbContext.Groups
             .AsNoTracking()
             .Where(x => x.GroupId == groupId)
             .FirstOrDefaultAsync();
    }

    public async Task<GroupMemberTbl?> GetMemberAsync(EntityId groupId, EntityId memberId)
    {
        return await appDbContext.GroupMembers
             .AsNoTracking()
             .Where(x => x.MemberId == memberId).Where(x => x.GroupId == groupId)
             .FirstOrDefaultAsync();
    }
}
