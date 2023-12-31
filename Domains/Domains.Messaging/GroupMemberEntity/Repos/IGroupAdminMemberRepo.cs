using Domains.Messaging.GroupRequestEntity;
using Domains.Messaging.Shared.Models;
using Domains.Messaging.Shared.ValueObjects;
using Shared.ValueObjects;

namespace Domains.Messaging.GroupMemberEntity.Repos;  
public interface IGroupAdminMemberRepo {

    Task ConfirmedRequest(GroupMemberTbl groupMemberEntity,GroupRequestTbl groupRequestEntity);
    Task DeleteMemberAsync(GroupMemberTbl entity);

    Task FreeAsync(GroupMemberTbl entity);
    Task BlockAsync(GroupMemberTbl entity , EntityId adminId , DateTime startBlockAt , DateTime? endBlockAt , string? reason);
    Task ConvertToAdminAsync(GroupMemberTbl entity, EntityId adminId , DateTime startBlockAt , DateTime? endBlockAt , string? reason);
    Task ConvertToRegularMember(GroupMemberTbl entity);
    Task<AdminMemberInfo?> GetAdminAsync(EntityId groupId , EntityId memberId);
    Task<List<AdminMemberResult>> GetAdminsAsync(EntityId groupId);
    Task<List<BlockMemberResult>> GetBlockedMembersAsync(EntityId groupId);

}
