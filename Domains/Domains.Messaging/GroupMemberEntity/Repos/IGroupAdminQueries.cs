using Domains.Messaging.GroupRequestEntity;
using Domains.Messaging.Shared.Models;
using Domains.Messaging.Shared.ValueObjects;
using Shared.ValueObjects;

namespace Domains.Messaging.GroupMemberEntity.Repos;
public interface IGroupAdminQueries {
    Task<List<GroupRequestTbl>> GetRequestsAsync(EntityId groupId);
    Task<AdminMemberInfo?> IsAdminAsync(EntityId groupId , EntityId memberId);
    Task<List<AdminMemberResult>> GetAdminsAsync(EntityId groupId);
    Task<List<BlockMemberResult>> GetBlockedMembersAsync(EntityId groupId);
}