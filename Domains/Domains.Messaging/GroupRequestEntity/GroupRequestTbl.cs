using Domains.Auth.UserEntity;
using Domains.Messaging.GroupEntity;
using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupRequestEntity.ValueObjects;
using Shared.Generics;
using Shared.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domains.Messaging.GroupRequestEntity;
[Table("GroupRequests")]
public record class GroupRequestTbl :IEntity {

    // Ids
    [Key]
    public GroupRequestId Id { get; set; }
    public EntityId RequesterId { get; set; }
    public GroupId GroupId { get; set; }

    // otherProps
    public uint RequestNumbers { get; set; } = 0;
    public string? Description { get; set; }
    public DateTime RequestedAt { get; set; }
    public bool IsBlocked { get; set; }


    [Timestamp]
    public byte[] Timestamp { get; set; }

    // Relation ships
    public AppUser Requester { get; set; }
    public GroupTbl Group { get; set; }


}
