﻿using Domains.Messaging.Shared.ValueObjects;

namespace Domains.Messaging.GroupEntity.ValueObjects;  
public record class MessageBlocking {
    public bool IsBlocked { get; set; } = false;
    public AppUserId? ByWhomId { get; set; }
    public DateTime? StartAt { get; set; }
    public DateTime? EndAt { get; set;}   


}
