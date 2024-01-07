using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupMemberEntity;
using Domains.Messaging.GroupRequestEntity;
using Domains.Messaging.Shared.ValueObjects;

namespace Domains.Messaging.GroupEntity.Repo;
public interface IGroupQueries {
    Task<GroupRequestTbl?> GetRequestAsync(GroupId groupId , AppUserId requestId);
    Task<List<GroupMemberTbl>> GetMembersAsync(GroupId groupId);
    Task<GroupMemberTbl?> GetMemberAsync(GroupId groupId , AppUserId memberId);
    Task<GroupTbl?> GetGroupAsync(GroupId groupId);
    Task<GroupTbl?> GetGroupByDisplayIdAsync(DisplayId displayId);
}
