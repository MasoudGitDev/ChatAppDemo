using Domains.Auth.UserEntity;
using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupMemberEntity.Entity;
using Domains.Messaging.GroupMessageEntity.Aggregate;
using Domains.Messaging.GroupRequestEntity;
using Domains.Messaging.Shared.ValueObjects;
using Shared.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Generics = Shared.Generics;

namespace Domains.Messaging.GroupEntity.Entity;

[Table("Groups")]
public partial record class GroupTbl : Generics.Entity {
    [Key]
    public GroupId GroupId { get; private set; }
    public EntityId CreatorId { get; private set; }


    public string Title { get; private set; }
    public DisplayId DisplayId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public LinkedList<Logo>? LogoURLs { get; private set; }
    public string? Description { get; private set; }
    public bool IsRequestable { get; private set; } = false;
    public LinkedList<string> Categories { get; private set; }

    public LinkedList<GroupMessageTbl> Messages { get; private set; }

    /// <summary>
    /// Store as Json
    /// </summary>
    public MessageLocking MessageLocking { get; private set; }

    [Timestamp]
    public byte[] Timestamp { get; private set; }

    // Relationships
    public AppUser Creator { get; private set; }
    public LinkedList<GroupRequestTbl> Requests { get; private set; }
    public LinkedList<GroupMemberTbl> Members { get; private set; }

}
