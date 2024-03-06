using Domains.Messaging.GroupEntity.Entity;
using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupMemberEntity.Entity;
using Domains.Messaging.GroupRequestEntity;
using Domains.Messaging.Shared.ValueObjects;
using Domains.Messaging.UnitOfWorks;
using Shared.Extensions;
using Shared.Models;

namespace Apps.Messaging.Shared.Manager;
internal abstract class BaseGroupManager<T, R>(IGroupMessagingUOW _unitOfWork) {
    protected async Task<AdminMemberInfo> GetAdminWithCheckingAsync(GroupId groupId , AppUserId adminId) {
        return ( await _unitOfWork.MemberQueries.GetAdminMemberInfoAsync(groupId , adminId) )
            .ThrowIfNull(new ExceptionModel("BaseGroupManager" , "GetAdminWithCheckingAsync" ,
                "NotFound" ,
                "You are not admin!"));
    }
    protected async Task<GroupMemberTbl> GetMemberWithCheckingAsync(GroupId groupId , AppUserId memberId) {
        return ( await _unitOfWork.MemberQueries.GetMemberAsync(groupId , memberId) )
            .ThrowIfNull(new ExceptionModel("BaseGroupManager" , "GetMemberWithCheckingAsync" ,
                "NotFound" ,
                $"Not found any members with this id :{memberId.Value}"));
    }
    protected async Task<GroupTbl> GetGroupWithCheckingAsync(GroupId groupId) {
        return ( await _unitOfWork.MemberQueries.GetGroupAsync(groupId) )
            .ThrowIfNull(new ExceptionModel("BaseGroupManager" , "GetGroupWithCheckingAsync" ,
                "NotFound" ,
                $"There is no any groups with this id :{groupId.Value} "));
    }
    protected async Task<GroupRequestTbl> GetRequestWithCheckingAsync(GroupId groupId , AppUserId requesterId) {

        return ( await _unitOfWork.RequestQueries.GetRequestAsync(groupId , requesterId) )
            .ThrowIfNull(new ExceptionModel("BaseGroupManager" , "GetRequestWithCheckingAsync" ,
                "NotFound" ,
                "NotFound any requests with those Ids."));
    }

    public abstract Task<R> Handle(T request , CancellationToken cancellationToken);
}
