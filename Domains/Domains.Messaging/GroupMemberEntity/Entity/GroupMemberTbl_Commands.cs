using Domains.Messaging.Shared.ValueObjects;
using Shared.Abstractions.Messaging.Constants;
using Shared.Exceptions;

namespace Domains.Messaging.GroupMemberEntity.Entity;
public partial record class GroupMemberTbl {

    // #Solve , use startAt 
    public void UnBlock(DateTime? startAt = null) {
        if(IsBlocked) {
            IsBlocked = false;
            BlockMemberInfo = null;
        }
        throw new NotPossibleException("You can not unblock some one when he/she was not blocked.");
    }

    /// <summary>
    ///  When you intend to block a member , The IsAdmin is Changing to false also. 
    /// </summary>
    public void Block(AppUserId adminId , DateTime? startAt , DateTime? endAt , string? reason) {
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
