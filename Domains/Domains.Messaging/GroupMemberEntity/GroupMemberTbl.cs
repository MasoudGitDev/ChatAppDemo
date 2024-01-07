using Domains.Auth.UserEntity;
using Domains.Messaging.GroupEntity;
using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupMemberEntity.ValueObjects;
using Domains.Messaging.Shared.ValueObjects;
using Shared.Generics;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domains.Messaging.GroupMemberEntity;
[Table("GroupMembers")]
public record class GroupMemberTbl : IEntity
{

    // Ids
    [Key]
    public GroupMemberId Id { get; init; }
    public GroupId GroupId { get; init; }
    public AppUserId MemberId { get; init; }

    //info
    public DateTime MemberAt { get; init; } = DateTime.UtcNow;

    public bool IsAdmin { get; set; } = false;
    public AdminMemberInfo? AdminInfo { get; set; }

    public bool IsBlocked { get; set; }
    public BlockedMemberInfo? BlockMemberInfo { get; set; }

    [Timestamp]
    public byte[] Timestamp { get; set; }

    // relation ships
    public AppUser Member { get; set; }
    public GroupTbl Group { get; set; }
}
