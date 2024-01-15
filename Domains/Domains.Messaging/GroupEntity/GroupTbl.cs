using Domains.Auth.UserEntity;
using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupMemberEntity;
using Domains.Messaging.GroupMessageEntity;
using Domains.Messaging.GroupRequestEntity;
using Domains.Messaging.Shared.ValueObjects;
using Shared.Generics;
using Shared.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domains.Messaging.GroupEntity;

[Table("Groups")]
public record class GroupTbl :IEntity{
    [Key]
    public GroupId GroupId { get; init; }
    public EntityId CreatorId { get; set; }


    public string Title { get; set; }
    public DisplayId DisplayId { get; set; }
    public DateTime CreatedAt {  get; init; }      
    public LinkedList<Logo>? Logos { get; set; }
    public string? Description { get; set; }
    public bool IsRequestable { get; set; } = false;
    public LinkedList<string> Categories { get; set; }

    public LinkedList<GroupMessageTbl> Messages { get; set; }

    [Timestamp]
    public byte[] Timestamp { get; set; }

    // Relationships
    public AppUser Creator { get; set; }
    public ICollection<GroupRequestTbl> Requests { get; set; }
    public LinkedList<GroupMemberTbl> Members { get; set; }

}
