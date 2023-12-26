using Domains.Auth.UserEntity;
using Domains.Messaging.Shared.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace Domains.Messaging.GroupEntity;
public record class GroupTbl {
    [Key]
    public string GroupId { get; init; } = string.Empty;
    public string DisplayId { get; set; } = string.Empty;

    public DateTime CreatedAt {  get; init; }
    public string CreatorId { get; set; }

    public string Pictures { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;


    public LinkedList<Admin> Admins { get; set; } = new();

    public LinkedList<Member> Members { get; set; } = new();
    public byte[] Timestamp { get; set; }

    // Relationships
    public AppUser Creator { get; set; }
   
}
