﻿using Domains.Auth.UserEntity;
using Domains.Messaging.GroupMemberEntity;
using Domains.Messaging.Shared.ValueObjects;
using Shared.Generics;
using Shared.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace Domains.Messaging.GroupEntity;
public record class GroupTbl :IEntity{
    [Key]
    public EntityId GroupId { get; init; }

    public string DisplayId { get; set; } = string.Empty;

    public DateTime CreatedAt {  get; init; }

    public EntityId CreatorId { get; set; }

    public LinkedList<Logo> Logos { get; set; } = new();
    public string? Description { get; set; }


    public LinkedList<GroupMemberTbl> Members { get; set; } = new();
    public byte[] Timestamp { get; set; }

    // Relationships
    public AppUser Creator { get; set; }
   
}
