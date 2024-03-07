using Domains.Messaging.Shared.ValueObjects;
using Shared.Abstractions.Messaging.Constants;
using Shared.Exceptions;

namespace Domains.Messaging.GroupMemberEntity.Entity;
public partial record class GroupMemberTbl {

    public void UnBlock() {
        IsBlocked = false;
        BlockMemberInfo = null;
    }

    /// <summary>
    ///  When you intend to block a member , The IsAdmin is Changing to false also. 
    /// </summary>
    public void Block(AppUserId adminId , DateTime? startAt = null , DateTime? endAt = null , string? reason = null) {
        AdminInfo = null;
        IsAdmin = false;
        IsBlocked = true;
        BlockMemberInfo = BlockedMemberInfo.Create(startAt , endAt , adminId , reason);
    }



    public void ToAdmin(
      AppUserId byWhomAdminId ,
      AdminLevel levelToAssign ,
      DateTime? startAt = null ,
      DateTime? endAt = null ,
      string? reason = null) {
        IsAdmin = true;
        AdminInfo = new() {
            Reason = reason ,
            StartAt = startAt ?? DateTime.UtcNow ,
            EndAt = endAt ,
            ByWhomId = byWhomAdminId ,
            AdminLevel = levelToAssign ,
        };
    }


    public void ToNormal() {
        IsAdmin = false;
        AdminInfo = null;
        IsBlocked = false;
        BlockMemberInfo = null;
    }
}
