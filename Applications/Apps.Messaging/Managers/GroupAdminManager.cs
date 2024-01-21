using Apps.Messaging.Exceptions;
using Domains.Messaging.GroupEntity;
using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupMemberEntity;
using Domains.Messaging.GroupMemberEntity.Repos;
using Domains.Messaging.GroupRequestEntity;
using Domains.Messaging.Shared.ValueObjects;
using MediatR;
using Shared.Abstractions.Messaging.Constants;
using Shared.Enums;
using Shared.Models;

namespace Apps.Messaging.Managers;
internal abstract class GroupAdminHandler<T,R>(IGroupAdminRepo groupAdminRepo)
    : IRequestHandler<T , R> where T : IRequest<R> where R : IResult {
    // Result base
    protected async Task<Result> TryToDoActionByAdminAsync(
        GroupId groupId ,
        AppUserId adminId ,
        AppUserId memberId ,
        Func<GroupMemberTbl , AdminAccessLevels , Task> actionAsync)
    {
        var findAdmin = await GetAdminWithCheckingAsync(groupId, adminId);
        var findMember = await GetMemberWithCheckingAsync(groupId, memberId);
        await actionAsync.Invoke(findMember, findAdmin.AccessLevel);
        return new Result(ResultStatus.Success, null);
    }
    protected async Task<Result> DeleteGroupAsync(GroupId groupId, AppUserId adminId)
    {
        var findAdmin = await GetAdminWithCheckingAsync(groupId, adminId);
        if (findAdmin.AccessLevel != AdminAccessLevels.Owner)
        {
            throw new GroupAdminManagerException("DeleteGroupAsync" , "NotAccess", "just creator can delete this group.");
        }
        var currentGroup = await groupAdminRepo.General.Queries.GetGroupAsync(groupId);
        if (currentGroup is null) {
            throw new GroupAdminManagerException("GetGroupAsync" , "NotFound" , "Not found any group with that groupId");
        }
        await groupAdminRepo.Commands.DeleteGroupAsync(currentGroup,
            await groupAdminRepo.General.Queries.GetMembersAsync(groupId),
            await groupAdminRepo.RequestRepo.Queries.GetGroupRequestsAsync(groupId));
        return new Result(ResultStatus.Success, null);
    }
    protected async Task<Result> TryToDoAsync(GroupId groupId , Func<GroupTbl,Task> actions) {
        var findGroup = await GetGroupWithCheckingAsync(groupId);
        await actions(findGroup);
        return new Result(ResultStatus.Success , null);
    }

    protected async Task<AdminMemberInfo> GetAdminWithCheckingAsync(GroupId groupId, AppUserId adminId)
    {
        var findAdmin = await groupAdminRepo.Queries.GetAdminMemberAsync(groupId, adminId);
        if (findAdmin == null)
        {
            throw new GroupAdminManagerException("GetAdminWithChecking", "NotFound", "You are not admin!");
        }
        return findAdmin;
    }
    protected async Task<GroupMemberTbl> GetMemberWithCheckingAsync(GroupId groupId, AppUserId memberId)
    {
        var findMember = await groupAdminRepo.General.Queries.GetMemberAsync(groupId, memberId);
        if (findMember == null)
        {
            throw new GroupAdminManagerException("GetMemberWithCheckingAsync", "NotFound", $"Not found any members with this id :{memberId.Value}");
        }
        return findMember;
    }
    protected async Task<GroupTbl> GetGroupWithCheckingAsync(GroupId groupId) {
        var group = await groupAdminRepo.General.Queries.GetGroupAsync(groupId);
        if(group == null) {
            throw new GroupAdminManagerException("GetGroupAsync" , "NotFound" , "There is no any groups with this id");
        }
        return group;
    }
    protected async Task<GroupRequestTbl> GetRequestWithCheckingAsync(GroupId groupId , AppUserId requesterId) {
        var groupRequest = await groupAdminRepo.RequestRepo.Queries.GetRequestAsync(groupId,requesterId);
        if(groupRequest == null) {
            throw new GroupAdminManagerException("GetRequestAsync" , "NotFound" , "NotFound any request with that Id.");
        }
        return groupRequest;
    }

    public abstract Task<R> Handle(T request , CancellationToken cancellationToken);
}
