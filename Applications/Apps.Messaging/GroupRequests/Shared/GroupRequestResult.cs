namespace Apps.Messaging.GroupRequests.Shared;
/// <summary>
/// If The Result Is For Requester, The Id Is GroupId!
/// If The Result Is For Group, The Id Is RequesterId!
/// </summary>
public record GroupRequestResult(string? Description , uint RequestNumbers , DateTime RequestedAt , bool IsBlocked , Guid Id);
