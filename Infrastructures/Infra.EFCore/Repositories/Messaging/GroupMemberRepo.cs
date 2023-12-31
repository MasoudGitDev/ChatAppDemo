
using Domains.Auth.UserEntity;
using Domains.Messaging.GroupEntity;
using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupMemberEntity;
using Domains.Messaging.GroupMemberEntity.Repos;
using Domains.Messaging.Shared.Models;
using Infra.EFCore.Contexts;
using Microsoft.EntityFrameworkCore;
using Shared.Abstractions.Messaging.Constants;
using Shared.Models;
using Shared.ValueObjects;

namespace Infra.EFCore.Repositories.Messaging {
    internal class GroupMemberRepo(AppDbContext appDbContext) : IGroupMemberRepo {

        public async Task<List<GroupInfo>> GetGroupsAsync(EntityId userId) {
            return await appDbContext.GroupMembers
                .AsNoTracking()
                .Include(x=>x.Group)
                .Where(x => x.MemberId == userId)
                .Select(x => new GroupInfo(x.GroupId , x.Group.DisplayId , x.Group.Logos.Last() , x.Group.Title))
                .ToListAsync();
        }

        public async Task<GroupMemberTbl?> GetMemberAsync(EntityId groupId , EntityId memberId) {
            return await appDbContext.GroupMembers
              .AsNoTracking()
              .Where(x => x.MemberId == memberId).Where(x => x.GroupId == groupId)
              .FirstOrDefaultAsync();
        }

        public async Task<List<MemberInfo>> GetMembersAsync(EntityId groupId) {
           return await appDbContext.GroupMembers
                .AsNoTracking()
                .Where(x => x.GroupId == groupId)
                .Select(x=> new MemberInfo(x.MemberId , x.MemberAt , x.IsAdmin , x.IsBlocked))
                .ToListAsync();
        }

        public async Task RemoveMemberAsync(GroupMemberTbl entity) {
            appDbContext.GroupMembers.Remove(entity);
            await appDbContext.SaveChangesAsync();
        }
    }
}
