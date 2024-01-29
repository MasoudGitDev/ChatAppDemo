using Domains.Messaging.GroupEntity;
using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupMemberEntity;
using Domains.Messaging.GroupMemberEntity.Repos;
using Domains.Messaging.GroupRequestEntity;
using Domains.Messaging.Shared.ValueObjects;
using Shared.Extensions;
using Shared.Models;

namespace Apps.Messaging.Shared.Manager;
internal abstract class BaseGroupManager<T, R>(IGroupAdminRepo groupAdminRepo) {
    protected async Task<AdminMemberInfo> GetAdminWithCheckingAsync(GroupId groupId , AppUserId adminId) {
        return ( await groupAdminRepo.Queries.GetAdminMemberAsync(groupId , adminId) )
            .IfIsNull(new ExceptionModel("BaseGroupManager" ,"GetAdminWithCheckingAsync" ,
                "NotFound" ,
                "You are not admin!"));
    }
    protected async Task<GroupMemberTbl> GetMemberWithCheckingAsync(GroupId groupId , AppUserId memberId) {
        return ( await groupAdminRepo.General.Queries.GetMemberAsync(groupId , memberId) )
            .IfIsNull(new ExceptionModel("BaseGroupManager" ,"GetMemberWithCheckingAsync" ,
                "NotFound" ,
                $"Not found any members with this id :{memberId.Value}"));
    }
    protected async Task<GroupTbl> GetGroupWithCheckingAsync(GroupId groupId) {
        return ( await groupAdminRepo.General.Queries.GetGroupAsync(groupId) )
            .IfIsNull(new ExceptionModel("BaseGroupManager" ,"GetGroupWithCheckingAsync" ,
                "NotFound" ,
                $"There is no any groups with this id :{groupId.Value} "));
    }
    protected async Task<GroupRequestTbl> GetRequestWithCheckingAsync(GroupId groupId , AppUserId requesterId) {

        return ( await groupAdminRepo.RequestRepo.Queries.GetRequestAsync(groupId , requesterId) )
            .IfIsNull(new ExceptionModel("BaseGroupManager" ,"GetRequestWithCheckingAsync" ,
                "NotFound" ,
                "NotFound any requests with those Ids."));
    }

    public abstract Task<R> Handle(T request , CancellationToken cancellationToken);
}
