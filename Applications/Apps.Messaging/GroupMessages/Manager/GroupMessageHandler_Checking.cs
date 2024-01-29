using Apps.Messaging.Exceptions;
using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupMessageEntity;
using Domains.Messaging.GroupMessageEntity.ValueObjects;
using Domains.Messaging.Shared.ValueObjects;

namespace Apps.Messaging.GroupMessages.Manager;

// GroupMessage Checking
internal abstract partial class GroupMessageHandler<T, R> {
    protected async Task CheckSendingAccessAsync(GroupId groupId , AppUserId memberId) {
        var group = await GetGroupWithCheckingAsync(groupId);
        if(group.MessageBlocking.IsBlocked) {
            var findAdmin = GetAdminWithCheckingAsync(groupId, memberId);
            if(findAdmin != null) {
                throw new GroupMessageHandlerException("BlockMessage" , "Just Admins can send any messages.");
            }
        }
    }
    protected async Task<GroupMessageTbl> GetMessageWithCheckingAsync(GroupMessageId groupMessageId) {
        var findMessage = await groupAdminRepo.MessageRepo.Queries.GetMessageByIdAsync(groupMessageId);
        if(findMessage == null) {
            throw new GroupMessageHandlerException("NotFound" , "There is no any messages with this groupMessageId");
        }
        return findMessage;
    }
}
