using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.Shared.ValueObjects;

namespace Apps.Messaging.GroupMessages.Shared;
public record MessageResult(GroupId GroupId , AppUserId memberId , string Message , string? FileUrl) {
}
