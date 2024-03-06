using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupMessageEntity.Aggregate;
using Domains.Messaging.GroupMessageEntity.Repos;
using Domains.Messaging.GroupMessageEntity.ValueObjects;
using Domains.Messaging.Shared.ValueObjects;
using Infra.EFCore.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infra.EFCore.Repositories.Messaging.GroupMessages;

internal class GroupMessageQueries(AppDbContext appDbContext) : IGroupMessageQueries
{
    public async Task<List<GroupMessageTbl>> GetGroupMessagesAsync(GroupId groupId, int messageCount = 50)
    {
        return await appDbContext.GroupMessages.AsNoTracking().Where(x => x.GroupId == groupId).Take(messageCount).ToListAsync();
    }

    public async Task<List<GroupMessageTbl>> GetMemberMessagesAsync(GroupId groupId , AppUserId memberId) {
        return await appDbContext.GroupMessages.AsNoTracking().Where(x => x.GroupId == groupId && x.AppUserId == memberId).ToListAsync();
    }

    public async Task<GroupMessageTbl?> GetCurrentMessageAsync(GroupMessageId messageId)
    {
        return await appDbContext.GroupMessages.AsNoTracking().Where(x => x.Id == messageId).FirstOrDefaultAsync();
    }

}
