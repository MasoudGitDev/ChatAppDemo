using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupMemberEntity;
using Domains.Messaging.Shared.ValueObjects;
using Shared.ValueObjects;

namespace Domains.Messaging.GroupEntity.Repo;
public interface IGroupQueries {
    Task<GroupId?> GetIdByDisplayIdAsync(DisplayId displayId);
    Task<List<GroupMemberTbl>> GetMembersAsync(GroupId groupId);
    Task<List<GroupMemberTbl>> GetMembersByDisplayIdAsync(DisplayId displayId);
    Task<GroupMemberTbl?> GetMemberAsync(GroupId groupId , AppUserId memberId);
    Task<GroupTbl?> GetGroupAsync(GroupId groupId);
    Task<GroupTbl?> GetGroupByDisplayIdAsync(DisplayId displayId);
}
