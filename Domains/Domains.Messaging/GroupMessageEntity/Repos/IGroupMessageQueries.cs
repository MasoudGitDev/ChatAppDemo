using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupMessageEntity.ValueObjects;
using Domains.Messaging.Shared.ValueObjects;

namespace Domains.Messaging.GroupMessageEntity.Repos;  
public interface IGroupMessageQueries {
    Task<GroupMessageTbl?> GetMessageByIdAsync(GroupMessageId messageId);
    Task<List<GroupMessageTbl>> GetGroupMessagesAsync(GroupId groupId);
    Task<List<GroupMessageTbl>> GetMemberMessagesByGroupIdAsync(GroupId groupId , AppUserId memberId);

}
