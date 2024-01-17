using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupMessageEntity;
using Domains.Messaging.GroupMessageEntity.Repos;
using Domains.Messaging.GroupMessageEntity.ValueObjects;
using Domains.Messaging.Shared.ValueObjects;
using Infra.EFCore.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infra.EFCore.Repositories.Messaging.Group.Messages;

internal class GroupMessageQueries(AppDbContext appDbContext) : IGroupMessageQueries {
    public async Task<List<GroupMessageTbl>> GetGroupMessagesAsync(GroupId groupId) {
        return await appDbContext.GroupMessages.AsNoTracking().Where(x=>x.GroupId == groupId).ToListAsync();
    }

    public async Task<List<GroupMessageTbl>> GetMemberMessagesByGroupIdAsync(GroupId groupId , AppUserId memberId) {
        return await appDbContext.GroupMessages.AsNoTracking().Where(x=>x.GroupId == groupId && x.AppUserId == memberId).ToListAsync();
    }

    public async Task<GroupMessageTbl?> GetMessageByIdAsync(GroupMessageId messageId) {
        return await appDbContext.GroupMessages.AsNoTracking().Where(x => x.Id == messageId).FirstOrDefaultAsync();
    }   
}
