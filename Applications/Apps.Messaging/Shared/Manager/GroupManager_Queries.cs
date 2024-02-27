using Domains.Messaging.GroupEntity.Entity;
using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupMemberEntity.Entity;
using Domains.Messaging.GroupRequestEntity;
using Domains.Messaging.Shared.ValueObjects;
using MediatR;
using Shared.Enums;
using Shared.Extensions;

namespace Apps.Messaging.GroupAdmins.Manager;
internal abstract partial class GroupManager<T, R> {

    protected async Task<(AdminMemberInfo, GroupMemberTbl)> GetAdminAndMemberWithCheckingAsync(
       GroupId groupId , AppUserId adminId , AppUserId memberId
       ) {
        var findAdmin = (await _unitOfWork.MemberQueries.GetAdminMemberInfoAsync(groupId, adminId))
            .ThrowIfNull("You are not admin!");
        var findMember = (await _unitOfWork.MemberQueries.GetMemberAsync(groupId, memberId))
            .ThrowIfNull($"Not found any members with id :{memberId.Value}");
        return (findAdmin, findMember);
    }


    protected async Task<GroupMemberTbl?> GetDeputyAdmin(GroupId groupId)
        => await _unitOfWork.MemberQueries.GetDeputyAdminAsync(groupId);

    protected async Task<GroupTbl?> GetGroupByDisplayIdAsync(DisplayId displayId)
        => await _unitOfWork.MemberQueries.GetGroupByDisplayIdAsync(displayId);

    protected async Task<List<GroupTbl>> FindGroupsByTitleAsync(string  title)
        => await _unitOfWork.MemberQueries.FindGroupsByTitleAsync(title);

    protected async Task<AdminMemberInfo?> GetAdminMemberInfoAsync(GroupId groupId , AppUserId adminId)
        => await _unitOfWork.MemberQueries.GetAdminMemberInfoAsync(groupId , adminId);

    protected async Task<GroupMemberTbl?> GetAdminMemberAsync(GroupId groupId , AppUserId adminId)
        => await _unitOfWork.MemberQueries.GetAdminMemberAsync(groupId , adminId);

    protected async Task<GroupTbl?> GetGroupAsync(GroupId groupId)
        => await _unitOfWork.MemberQueries.GetGroupAsync(groupId);

    protected async Task<List<GroupMemberTbl>> GetMembersAsync(GroupId groupId)
        => await _unitOfWork.MemberQueries.GetMembersAsync(groupId);


    #region Requests

    protected async Task<GroupRequestTbl?> GetRequestAsync(GroupId groupId , AppUserId requesterId)
        => await _unitOfWork.RequestQueries.GetRequestAsync(groupId , requesterId);

    protected async Task<List<GroupRequestTbl>> GetGroupRequestsAsync(
        GroupId groupId ,
        Visibility visible = Visibility.Visible)
        => await _unitOfWork.RequestQueries.GetGroupRequestsAsync(groupId,visible);    

    protected async Task<List<GroupRequestTbl>> GetUserRequestsAsync(
        AppUserId requesterId ,
        Visibility visible = Visibility.Visible)
       => await _unitOfWork.RequestQueries.GetUserRequestsAsync(requesterId,visible);

    #endregion

    protected async Task<List<GroupTbl>> GetUserGroupsAsync(AppUserId appUserId)
        => await _unitOfWork.MemberQueries.GetUserGroupsAsync(appUserId);


    protected async Task<GroupMemberTbl?> GetMemberAsync(GroupId groupId , AppUserId memberId)
        => await _unitOfWork.MemberQueries.GetMemberAsync(groupId , memberId);

    protected async Task<GroupRequestTbl?> GetGroupRequestAsync(GroupId groupId , AppUserId requesterId)
       => await _unitOfWork.RequestQueries.GetRequestAsync(groupId , requesterId);


}
