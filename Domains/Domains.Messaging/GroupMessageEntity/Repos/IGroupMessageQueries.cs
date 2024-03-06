using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupMessageEntity.Aggregate;
using Domains.Messaging.GroupMessageEntity.ValueObjects;
using Domains.Messaging.Shared.ValueObjects;

namespace Domains.Messaging.GroupMessageEntity.Repos;
public interface IGroupMessageQueries {
    Task<GroupMessageTbl?> GetCurrentMessageAsync(GroupMessageId messageId);
    Task<List<GroupMessageTbl>> GetMemberMessagesAsync(GroupId groupId , AppUserId memberId);
    Task<List<GroupMessageTbl>> GetGroupMessagesAsync(GroupId groupId , int messageCount = 50);
    

}
