using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.Shared.Models;
using Domains.Messaging.Shared.ValueObjects;

namespace Domains.Messaging.GroupMemberEntity.Repos;
public interface IGroupAdminQueries {
    Task<AdminMemberInfo?> GetAdminMemberAsync(GroupId groupId , AppUserId memberId);
    Task<List<AdminMemberResult>> GetAdminsAsync(GroupId groupId);
    Task<List<BlockMemberResult>> GetBlockedMembersAsync(GroupId groupId);
}