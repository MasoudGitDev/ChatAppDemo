using Domains.Auth.UserEntity;
using Domains.Messaging.GroupMemberEntity;
using Domains.Messaging.GroupRequesterEntity;
using Domains.Messaging.Shared.ValueObjects;
using Shared.Generics;
using Shared.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domains.Messaging.GroupEntity;

[Table("Groups")]
public record class GroupTbl :IEntity{
    [Key]
    public EntityId GroupId { get; init; }
    public EntityId CreatorId { get; set; }


    public string Title { get; set; } = string.Empty;
    public string DisplayId { get; set; } = string.Empty;
    public DateTime CreatedAt {  get; init; }      
    public LinkedList<Logo> Logos { get; set; } = new();
    public string? Description { get; set; }
    public bool CanAcceptAllRequests { get; set; } = false;


    
    public byte[] Timestamp { get; set; }

    // Relationships
    public AppUser Creator { get; set; }
    public ICollection<GroupRequesterTbl> Requesters { get; set; }
    public LinkedList<GroupMemberTbl> Members { get; set; }= new();

}
