using Domains.Auth.UserEntity;
using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.Shared.ValueObjects;
using Shared.Generics;
using System.ComponentModel.DataAnnotations;

namespace Domains.Messaging.GroupEntity;
public record class GroupTbl :IEntity{
    [Key]
    public GroupId GroupId { get; init; }

    public string DisplayId { get; set; } = string.Empty;

    public DateTime CreatedAt {  get; init; }

    public string CreatorId { get; set; }

    public LinkedList<Logo> Logos { get; set; } = new();
    public string Description { get; set; } = string.Empty;


    public LinkedList<Admin> Admins { get; set; } = new();

    public LinkedList<Member> Members { get; set; } = new();
    public byte[] Timestamp { get; set; }

    // Relationships
    public AppUser Creator { get; set; }
   
}
