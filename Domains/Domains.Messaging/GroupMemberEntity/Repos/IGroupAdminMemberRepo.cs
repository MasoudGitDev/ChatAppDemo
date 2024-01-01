using Domains.Messaging.GroupRequestEntity;
using Domains.Messaging.Shared.Models;
using Domains.Messaging.Shared.ValueObjects;
using Shared.Abstractions.Messaging.Constants;
using Shared.ValueObjects;

namespace Domains.Messaging.GroupMemberEntity.Repos;  
public interface IGroupAdminRepo {

    Task ConfirmedRequest(GroupMemberTbl groupMemberEntity,GroupRequestTbl groupRequestEntity);
    Task DeleteMemberAsync(GroupMemberTbl entity, AdminAccessLevels accessLevel);
    Task FreeAsync(GroupMemberTbl entity);
    Task BlockAsync(GroupMemberTbl entity , EntityId adminId , DateTime startAt , DateTime? endAt , string? reason);
    Task ConvertMemberToAdminAsync(GroupMemberTbl member, EntityId adminId , AdminAccessLevels accessLevel , DateTime startAt , DateTime? endAt , string? reason);
    Task ConvertAdminToMemberAsync(GroupMemberTbl adminMember , AdminAccessLevels accessLevels);

    Task<AdminMemberInfo?> GetAdminAsync(EntityId groupId , EntityId memberId);
    Task<List<AdminMemberResult>> GetAdminsAsync(EntityId groupId);
    Task<List<BlockMemberResult>> GetBlockedMembersAsync(EntityId groupId);
    Task<GroupRequestTbl?> GetRequestAsync(EntityId groupId , EntityId requesterId);

}
