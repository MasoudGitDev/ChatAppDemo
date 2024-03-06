using Domains.Messaging.GroupMemberEntity.Entity;
using Domains.Messaging.Shared.ValueObjects;
using Shared.Abstractions.Messaging.Constants;
using Shared.Exceptions;

namespace Apps.Messaging.Shared.Strategies;
internal abstract class ABSAdminStrategy
{

    internal enum StrategyResult
    {
        Succeeded, NeedDeputy, NotSameIds
    }

    public StrategyResult NeedDeputy(
        GroupMemberTbl owner,
        GroupMemberTbl? deputy,
        Action? changeOwnerTo)
    {
        if (deputy is null)
        {
            throw new NotPossibleException("Your group must have a deputy.");
        }
        if (owner.AdminInfo!.AdminLevel != AdminLevel.Owner)
        {
            throw new NotPossibleException("You must be an owner.");
        }
        if (changeOwnerTo is not null)
        {
            deputy.ToAdmin(owner.MemberId.Value, AdminLevel.Owner, reason: "Owner don't want to be an owner.");
            changeOwnerTo.Invoke();
            return StrategyResult.Succeeded;
        }
        throw new UnknownActionException();
    }


    public virtual StrategyResult CheckConditions(
        GroupMemberTbl admin,
        GroupMemberTbl targetMember,
        AdminLevel? levelToAssign = null)
    {

        if (levelToAssign is not null)
        {
            if (targetMember.IsBlocked)
            {
                throw new NotPossibleException("The blocked member can not be an admin.");
            }
            CheckLevelToAssignConditions(admin.AdminInfo!.AdminLevel, targetMember, levelToAssign);
            if (targetMember.IsAdmin is false)
            {
                return StrategyResult.Succeeded;
            }
        }
        return CheckIsNotInSameLevelOrHigher(admin, targetMember);
    }

    protected virtual StrategyResult CheckUserIdsEquality(GroupMemberTbl admin, AppUserId targetMemberId)
    {
        if (admin.MemberId.Value == targetMemberId.Value)
        {
            bool isOwner = admin.AdminInfo!.AdminLevel is AdminLevel.Owner;
            return isOwner ? StrategyResult.NeedDeputy : StrategyResult.Succeeded;
        }
        return StrategyResult.NotSameIds;
    }

    // private methods
    private StrategyResult CheckIsNotInSameLevelOrHigher(GroupMemberTbl admin, GroupMemberTbl targetMember)
    {
        bool isUsersEqual = targetMember.MemberId.Value == admin.MemberId.Value;
        if (isUsersEqual)
        {
            var result = CheckUserIdsEquality(admin, targetMember.MemberId.Value);
            if (result is not StrategyResult.NotSameIds)
            {
                return result;
            }
        }

        bool isSameOrHigherLevel =
            targetMember.IsAdmin &&
            targetMember.AdminInfo != null &&
            targetMember.AdminInfo.AdminLevel >= admin.AdminInfo!.AdminLevel;

        return isSameOrHigherLevel ?
             throw new NotPossibleException("You can not change state of other in same or higher level.") :
             StrategyResult.Succeeded;
    }

    private void CheckLevelToAssignConditions(
       AdminLevel adminLevel, GroupMemberTbl targetMember, AdminLevel? levelToAssign)
    {

        if (levelToAssign is null)
        {
            throw new NotPossibleException("You must choose a proper <admin-level>.");
        }

        // very important check
        if (levelToAssign >= adminLevel)
        {
            throw new NotPossibleException(
                $"You can not change your or others <admin-level> state to {levelToAssign} or higher.");
        }

        var isTargetInSameLevel =
            targetMember.IsAdmin &&
            targetMember.AdminInfo != null &&
            targetMember.AdminInfo.AdminLevel == levelToAssign;

        if (isTargetInSameLevel)
        {
            throw new NotPossibleException(
                $"The [target-admin-level = {targetMember.AdminInfo?.AdminLevel} ] is equal to {levelToAssign}.");
        }
    }



}
