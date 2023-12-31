using Domains.Auth.UserEntity;
using Domains.Messaging.GroupEntity;
using Domains.Messaging.Shared.ValueObjects;
using Shared.Generics;
using Shared.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domains.Messaging.GroupMemberEntity;
[Table("GroupMembers")]
public record class GroupMemberTbl : IEntity
{

    // Ids
    [Key]
    public EntityId Id { get; init; }
    public EntityId GroupId { get; init; }
    public EntityId MemberId { get; init; }

    //info
    public DateTime MemberAt { get; init; } = DateTime.UtcNow;

    public bool IsAdmin { get; set; } = false;
    public AdminMemberInfo? AdminInfo { get; set; }

    public bool IsBlocked { get; set; }
    public BlockMemberInfo? BlockMemberInfo { get; set; }


    // relation ships
    public AppUser Member { get; set; }
    public GroupTbl Group { get; set; }



}
