using Domains.Messaging.GroupEntity.Entity;
using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupMemberEntity.Entity;
using Domains.Messaging.Shared.Models;
using Domains.Messaging.Shared.ValueObjects;

namespace Domains.Messaging.GroupMemberEntity.Repos;
public interface IGroupMemberQueries
{
    Task<GroupId?> GetIdByDisplayIdAsync(DisplayId displayId);
    Task<List<GroupMemberTbl>> GetMembersAsync(GroupId groupId);
    Task<List<GroupMemberTbl>> GetMembersByDisplayIdAsync(DisplayId displayId);
    Task<GroupMemberTbl?> GetMemberAsync(GroupId groupId, AppUserId memberId);
    Task<GroupTbl?> GetGroupAsync(GroupId groupId);
    Task<GroupTbl?> GetGroupByDisplayIdAsync(DisplayId displayId);
    Task<List<GroupTbl>> GetUserGroupsAsync(AppUserId userId);
    Task<List<GroupTbl>> FindGroupsByTitleAsync(string title);

    //admin Queries
    Task<AdminMemberInfo?> GetAdminMemberInfoAsync(GroupId groupId, AppUserId memberId);
    Task<GroupMemberTbl?> GetAdminMemberAsync(GroupId groupId , AppUserId memberId);
    Task<List<AdminMemberResult>> GetAdminsAsync(GroupId groupId);
    Task<List<BlockMemberResult>> GetBlockedMembersAsync(GroupId groupId);
    Task<GroupMemberTbl?> GetDeputyAdminAsync(GroupId groupId);
}
