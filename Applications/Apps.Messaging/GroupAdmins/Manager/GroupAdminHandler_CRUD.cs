using Apps.Messaging.Exceptions;
using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupEntity;
using Domains.Messaging.GroupMemberEntity;
using Domains.Messaging.Shared.ValueObjects;
using Shared.Abstractions.Messaging.Constants;
using Shared.Enums;
using Shared.Models;

namespace Apps.Messaging.GroupAdmins.Manager;
internal abstract partial class GroupAdminHandler<T, R> {
    protected async Task<Result> TryToDoActionByAdminAsync(
      GroupId groupId ,
      AppUserId adminId ,
      AppUserId memberId ,
      Func<GroupMemberTbl , AdminAccessLevels , Task> actionAsync) {
        var findAdmin = await GetAdminWithCheckingAsync(groupId, adminId);
        var findMember = await GetMemberWithCheckingAsync(groupId, memberId);
        await actionAsync.Invoke(findMember , findAdmin.AccessLevel);
        return new Result(ResultStatus.Success , null);
    }
    protected async Task<Result> DeleteGroupAsync(GroupId groupId , AppUserId adminId) {
        var findAdmin = await GetAdminWithCheckingAsync(groupId, adminId);
        if(findAdmin.AccessLevel != AdminAccessLevels.Owner) {
            throw new GroupAdminManagerException("NotAccess" , "just creator can delete this group.");
        }
        var currentGroup = await groupAdminRepo.General.Queries.GetGroupAsync(groupId);
        if(currentGroup is null) {
            throw new GroupAdminManagerException("NotFound" , "Not found any group with that groupId");
        }
        await groupAdminRepo.Commands.DeleteGroupAsync(currentGroup ,
            await groupAdminRepo.General.Queries.GetMembersAsync(groupId) ,
            await groupAdminRepo.RequestRepo.Queries.GetGroupRequestsAsync(groupId));
        return new Result(ResultStatus.Success , null);
    }
    protected async Task<Result> TryToDoAsync(GroupId groupId , Func<GroupTbl , Task> actions) {
        var findGroup = await GetGroupWithCheckingAsync(groupId);
        await actions(findGroup);
        return new Result(ResultStatus.Success , null);
    }

}
