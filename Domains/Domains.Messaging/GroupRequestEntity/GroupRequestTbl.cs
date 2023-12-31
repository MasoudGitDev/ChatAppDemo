using Domains.Auth.UserEntity;
using Domains.Messaging.GroupEntity;
using Shared.Generics;
using Shared.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domains.Messaging.GroupRequestEntity;
[Table("GroupRequesters")]
public record class GroupRequestTbl :IEntity {

    // Ids
    [Key]
    public EntityId Id { get; set; }
    public EntityId RequesterId { get; set; }
    public EntityId GroupId { get; set; }

    // otherProps
    public uint RequestNumbers { get; set; } = 0;
    public string? Description { get; set; }
    public DateTime RequestedAt { get; set; }
    public bool IsBlocked { get; set; }

    // Relation ships
    public AppUser Requester { get; set; }
    public GroupTbl Group { get; set; }


}
