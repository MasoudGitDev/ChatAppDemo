using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupMemberEntity;
using Domains.Messaging.Shared.UnitOfWorks;
using Domains.Messaging.Shared.ValueObjects;
using Shared.Abstractions.Messaging.Constants;
using Shared.Enums;
using Shared.Models;

namespace Apps.Messaging.GroupAdmins.Shared;  
internal abstract class GroupAdminManager(IGroupUnitOfWork groupUnitOfWork) {
    protected async Task<Result> TryToDoActionByAdminAsync(
        Guid groupId, Guid adminId , Guid memberId , Func<GroupMemberTbl,AdminAccessLevels,Task> actionAsync ) {
        var findAdmin =await CheckAdminAsync( groupId, adminId );
        if(findAdmin.Status == ResultStatus.Failed) {
            return new Result(findAdmin.Status , findAdmin.Error);
        }
        var findMember = await CheckMemberAsync(groupId , memberId);
        if(findMember.Status == ResultStatus.Failed) {
            return new Result(findMember.Status ,findMember.Error);
        }
        await actionAsync.Invoke(findMember.Content!,findAdmin.Content!.AccessLevel);
        return new Result(ResultStatus.Success ,null);
    }

  

    protected async Task<Result<AdminMemberInfo>> CheckAdminAsync(Guid groupId , Guid adminId) {
        var findAdmin =await groupUnitOfWork.AdminRepo.GetAdminAsync(groupId,adminId);
        if(findAdmin == null) {
            return new Result<AdminMemberInfo>(
                ResultStatus.Failed , new("CheckIsAdminAsync" , "NotAccess" , "You are not an admin.") , null);
        }
        return new Result<AdminMemberInfo>(
                ResultStatus.Success , null, findAdmin);
    }
    protected async Task<Result<GroupMemberTbl>>  CheckMemberAsync(Guid groupId, Guid memberId) {
        var findMember = await groupUnitOfWork.MemberRepo.GetMemberAsync(groupId , memberId);
        if(findMember == null) {
            return new Result<GroupMemberTbl>(ResultStatus.Failed ,
                     new("CheckMemberAsync" , "NotFound" , $"Not found any members with this id :{memberId}") , null);
        }
        return new Result<GroupMemberTbl>(ResultStatus.Success, null, findMember);
    } 

}
