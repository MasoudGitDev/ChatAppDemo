using Domains.Auth.UserEntity;
using Domains.Messaging.GroupEntity;
using Shared.Generics;
using Shared.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace Domains.Messaging.GroupRequesterEntity;
public record class GroupRequesterTbl :IEntity {

    // Ids
    [Key]
    public EntityId Id { get; set; }
    public EntityId RequesterId { get; set; }
    public EntityId GroupId { get; set; }

    // otherProps
    public uint RequestNumbers { get; set; } = 0;

    // Relation ships
    public AppUser Requester { get; set; }
    public GroupTbl Group { get; set; }


}
