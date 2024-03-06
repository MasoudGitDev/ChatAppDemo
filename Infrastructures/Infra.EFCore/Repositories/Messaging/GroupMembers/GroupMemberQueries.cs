using Domains.Messaging.GroupEntity.Entity;
using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupMemberEntity.Entity;
using Domains.Messaging.GroupMemberEntity.Repos;
using Domains.Messaging.Shared.Models;
using Domains.Messaging.Shared.ValueObjects;
using Infra.EFCore.Contexts;
using Microsoft.EntityFrameworkCore;
using Shared.Abstractions.Messaging.Constants;

namespace Infra.EFCore.Repositories.Messaging.GroupMembers;
internal class GroupMemberQueries(AppDbContext appDbContext) : IGroupMemberQueries
{
    public async Task<List<GroupMemberTbl>> GetMembersAsync(GroupId groupId)
    {
        return await appDbContext.GroupMembers.AsNoTracking()
            .Where(x => x.GroupId == groupId).ToListAsync();
    }
    public async Task<GroupTbl?> GetGroupAsync(GroupId groupId)
    {
        return await appDbContext.Groups.AsNoTracking()
            .Where(x => x.GroupId == groupId).FirstOrDefaultAsync();
    }

    public async Task<GroupMemberTbl?> GetMemberAsync(GroupId groupId, AppUserId memberId)
    {
        return await appDbContext.GroupMembers.AsNoTracking()
            .Where(x => x.GroupId == groupId && x.MemberId == memberId).FirstOrDefaultAsync();
    }

    public async Task<GroupTbl?> GetGroupByDisplayIdAsync(DisplayId displayId)
    {
        return await appDbContext.Groups.AsNoTracking()
            .Where(x => x.DisplayId == displayId).FirstOrDefaultAsync();
    }

    public async Task<GroupId?> GetIdByDisplayIdAsync(DisplayId displayId)
    {
        return await appDbContext.Groups.AsNoTracking()
            .Where(x => x.DisplayId == displayId).Select(x => x.GroupId).FirstOrDefaultAsync();
    }

    public async Task<List<GroupMemberTbl>> GetMembersByDisplayIdAsync(DisplayId displayId)
    {
        return await appDbContext.GroupMembers.AsNoTracking().Include(x => x.Group)
                    .Where(x => x.Group.DisplayId == displayId && !x.IsBlocked).ToListAsync();
    }

    public async Task<List<GroupTbl>> GetUserGroupsAsync(AppUserId userId)
    {
        return await appDbContext.GroupMembers.AsNoTracking().Include(x => x.Group)
            .Where(x => x.MemberId == userId).Select(x => x.Group).ToListAsync();
    }

    public async Task<List<GroupTbl>> FindGroupsByTitleAsync(string title)
    {
        return await appDbContext.Groups.AsNoTracking()
            .Where(x => x.Title.Contains(title)).ToListAsync();
    }

  
    public async Task<List<AdminMemberResult>> GetAdminsAsync(GroupId groupId) {
        return await appDbContext.GroupMembers.AsNoTracking().Where(x => x.GroupId == groupId && x.IsAdmin)
          .Select(x => new AdminMemberResult(x.MemberId , x.AdminInfo!)).ToListAsync();
    }

   
    public async Task<AdminMemberInfo?> GetAdminMemberInfoAsync(GroupId groupId , AppUserId memberId) {
        return await appDbContext.GroupMembers
            .AsNoTracking()
            .Where(x => x.GroupId == groupId && x.MemberId == memberId && x.IsAdmin)
            .Select(x => x.AdminInfo).FirstOrDefaultAsync();
    }
    public async Task<GroupMemberTbl?> GetAdminMemberAsync(GroupId groupId , AppUserId memberId) {
        return await appDbContext.GroupMembers
            .AsNoTracking()
            .Where(x => x.GroupId == groupId && x.MemberId == memberId && x.IsAdmin)
            .FirstOrDefaultAsync();
    }

    //================== just for main admin or all admins
    public async Task<List<BlockMemberResult>> GetBlockedMembersAsync(GroupId groupId) {
        return await appDbContext.GroupMembers.AsNoTracking().Where(x => x.GroupId == groupId && x.IsBlocked == true)
        .Select(x => new BlockMemberResult(x.MemberId , x.BlockMemberInfo!)).ToListAsync();
    }

    public async Task<GroupMemberTbl?> GetDeputyAdminAsync(GroupId groupId) {
        return await appDbContext.GroupMembers
            .AsNoTracking()
            .Where(x => x.GroupId == groupId)
            .Where(x=> x.IsAdmin && x.AdminInfo != null && x.AdminInfo.AdminLevel == AdminLevel.Deputy)
            .FirstOrDefaultAsync();
    }
}
