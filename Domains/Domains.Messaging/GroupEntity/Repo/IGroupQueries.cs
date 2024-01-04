using Domains.Messaging.GroupMemberEntity;
using Domains.Messaging.GroupRequestEntity;
using Shared.ValueObjects;

namespace Domains.Messaging.GroupEntity.Repo;
public interface IGroupQueries {
    Task<GroupRequestTbl?> GetRequestAsync(EntityId groupId , EntityId requesterId);
    Task<List<GroupMemberTbl>> GetMembersAsync(EntityId groupId);
    Task<GroupMemberTbl?> GetMemberAsync(EntityId groupId , EntityId memberId);
    Task<GroupTbl?> GetGroupAsync(EntityId groupId);
}
