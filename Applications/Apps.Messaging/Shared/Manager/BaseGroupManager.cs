using Apps.Messaging.Exceptions;
using Domains.Messaging.GroupEntity;
using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupMemberEntity;
using Domains.Messaging.GroupMemberEntity.Repos;
using Domains.Messaging.GroupRequestEntity;
using Domains.Messaging.Shared.ValueObjects;

namespace Apps.Messaging.Shared.Manager;
internal abstract class BaseGroupManager<T, R>(IGroupAdminRepo groupAdminRepo) {
    protected async Task<AdminMemberInfo> GetAdminWithCheckingAsync(GroupId groupId , AppUserId adminId) {
        var findAdmin = await groupAdminRepo.Queries.GetAdminMemberAsync(groupId, adminId);
        if(findAdmin == null) {
            throw new GroupAdminManagerException("GetAdminWithChecking" , "NotFound" , "You are not admin!");
        }
        return findAdmin;
    }
    protected async Task<GroupMemberTbl> GetMemberWithCheckingAsync(GroupId groupId , AppUserId memberId) {
        var findMember = await groupAdminRepo.General.Queries.GetMemberAsync(groupId, memberId);
        if(findMember == null) {
            throw new GroupAdminManagerException("GetMemberWithCheckingAsync" , "NotFound" , $"Not found any members with this id :{memberId.Value}");
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
        var groupRequest = await groupAdminRepo.RequestRepo.Queries.GetRequestAsync(groupId, requesterId);
        if(groupRequest == null) {
            throw new GroupAdminManagerException("GetRequestAsync" , "NotFound" , "NotFound any request with that Id.");
        }
        return groupRequest;
    }

    public abstract Task<R> Handle(T request , CancellationToken cancellationToken);
}
