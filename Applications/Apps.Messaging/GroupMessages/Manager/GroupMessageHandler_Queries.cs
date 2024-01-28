using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupMessageEntity;
using Domains.Messaging.GroupMessageEntity.ValueObjects;
using Domains.Messaging.Shared.ValueObjects;

namespace Apps.Messaging.GroupMessages.Manager;

// GroupMessage Queries
internal abstract partial class GroupMessageHandler<T, R> {
    protected async Task<List<GroupMessageTbl>> GetGroupMessagesAsync(GroupId groupId , int messageCount = 50) {
        return await groupAdminRepo.MessageRepo.Queries.GetGroupMessagesAsync(groupId , messageCount);
    }
    protected async Task<List<GroupMessageTbl>> GetMemberMessagesAsync(GroupId groupId , AppUserId memberId) {
        return await groupAdminRepo.MessageRepo.Queries.GetMemberMessagesByGroupIdAsync(groupId , memberId);
    }
    protected async Task<GroupMessageTbl?> GetMessageAsync(GroupMessageId messageId) {
        return await groupAdminRepo.MessageRepo.Queries.GetMessageByIdAsync(messageId);
    }
}
