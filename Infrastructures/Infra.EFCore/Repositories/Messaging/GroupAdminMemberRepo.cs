using Domains.Messaging.GroupMemberEntity;
using Domains.Messaging.GroupMemberEntity.Repos;
using Domains.Messaging.GroupRequestEntity;
using Domains.Messaging.Shared.Models;
using Domains.Messaging.Shared.ValueObjects;
using Infra.EFCore.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Shared.ValueObjects;
using System.Drawing;

namespace Infra.EFCore.Repositories.Messaging;
internal class GroupAdminMemberRepo(AppDbContext appDbContext) : IGroupAdminMemberRepo {
    public async Task BlockAsync(GroupMemberTbl entity , EntityId adminId , DateTime startBlockAt , DateTime? endBlockAt , string? reason) {
        entity.IsBlocked = true;
        entity.BlockMemberInfo = new BlockMemberInfo() {
            Reason = reason ,
            BlockedById = adminId ,
            StartBlockAt = startBlockAt ,
            EndBlockAt = endBlockAt
        };
        appDbContext.GroupMembers.Update(entity);
        await appDbContext.SaveChangesAsync();
    }
    public async Task FreeAsync(GroupMemberTbl entity) {
        entity.IsBlocked = false;
        entity.BlockMemberInfo = null;
        appDbContext.GroupMembers.Update(entity);
        await appDbContext.SaveChangesAsync();
    }

    public Task ConfirmedRequest(GroupMemberTbl groupMemberEntity , GroupRequestTbl groupRequestEntity) {
        throw new NotImplementedException();
    }

    public async Task ConvertToAdminAsync(GroupMemberTbl entity , EntityId adminId , DateTime startBlockAt , DateTime? endBlockAt , string? reason) {
        entity.IsAdmin = true;
        entity.AdminInfo = new() {
            Reason = reason ,
            StartAdminAt= startBlockAt ,
            EndAdminAt= endBlockAt ,
            ByWhomId= adminId
        };
        appDbContext.GroupMembers.Update(entity);
        await appDbContext.SaveChangesAsync();
    }

    public async Task ConvertToRegularMember(GroupMemberTbl entity) {
        entity.IsAdmin = false;
        entity.AdminInfo = null;
        appDbContext.GroupMembers.Update(entity);
        await appDbContext.SaveChangesAsync();
    }

    public async Task DeleteMemberAsync(GroupMemberTbl entity) {
         appDbContext.GroupMembers.Remove(entity);
         await appDbContext.SaveChangesAsync();
    }

   

    // Queries
    public async Task<AdminMemberInfo?> GetAdminAsync(EntityId groupId , EntityId memberId) {
        return await appDbContext.GroupMembers.AsNoTracking()
            .Where(x=>x.GroupId == groupId).Where(x=>x.MemberId == memberId)
            .Select(x=> x.AdminInfo).SingleOrDefaultAsync();
    }
    public async Task<List<AdminMemberResult>> GetAdminsAsync(EntityId groupId) {
        return await appDbContext.GroupMembers.AsNoTracking()
            .Where(x => x.GroupId == groupId).Where(x=> x.IsAdmin == true)            
            .Select(x=> new AdminMemberResult(x.MemberId ,x.AdminInfo!)).ToListAsync();
    }
    public async Task<List<BlockMemberResult>> GetBlockedMembersAsync(EntityId groupId) {
        return await appDbContext.GroupMembers.AsNoTracking()
            .Where(x => x.GroupId == groupId).Where(x => x.IsBlocked == true)
            .Select(x => new BlockMemberResult(x.MemberId , x.BlockMemberInfo!)).ToListAsync();
    }

}


