using Domains.Auth.UserEntity;
using Domains.Messaging.GroupEntity.Entity;
using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupMemberEntity.ValueObjects;
using Domains.Messaging.Shared.ValueObjects;
using Shared.Abstractions.Messaging.Constants;
using Shared.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Generics = Shared.Generics;

namespace Domains.Messaging.GroupMemberEntity.Entity;
[Table("GroupMembers")]
public partial record class GroupMemberTbl : Generics.Entity {

    // Ids
    [Key]
    public GroupMemberId Id { get; private set; }
    public GroupId GroupId { get; private set; }
    public EntityId MemberId { get; private set; }

    //info
    public DateTime MemberAt { get; private set; }

    public bool IsAdmin { get; private set; } = false;
    public AdminMemberInfo? AdminInfo { get; private set; }

    public bool IsBlocked { get; set; }
    public BlockedMemberInfo? BlockMemberInfo { get; set; }

    [Timestamp]
    public byte[] Timestamp { get; private set; }


    public static GroupMemberTbl Create(
        GroupId groupId ,
        AppUserId memberId
        )
        => new() {
            Id = GroupMemberId.Create() ,
            GroupId = groupId ,
            MemberId = memberId ,
            MemberAt = DateTime.UtcNow ,
            IsAdmin = false ,
            AdminInfo = null ,
            IsBlocked = false ,
            BlockMemberInfo = null ,
        };

    public static GroupMemberTbl Create(
        GroupId groupId ,
        AppUserId memberId ,
        AdminLevel adminType ,
        string? reason = null
        )
        {
        return new() {
            Id = GroupMemberId.Create() ,
            GroupId = groupId ,
            MemberId = memberId ,
            MemberAt = DateTime.UtcNow ,
            IsAdmin = true ,
            AdminInfo = new AdminMemberInfo(
                adminType ,
                DateTime.UtcNow , null ,
                memberId ,
                reason) ,
            IsBlocked = false ,
            BlockMemberInfo = null ,
        };
    }

    // relation ships
    public AppUser Member { get; private set; }
    public GroupTbl Group { get; private set; }
}
