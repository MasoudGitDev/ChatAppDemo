using Domains.Auth.UserEntity;
using Domains.Messaging.GroupEntity;
using Shared.Abstractions.Messaging.Constants;
using Shared.Models;
using Shared.ValueObjects;

namespace Domains.Messaging.GroupMemberEntity.Repos;
public interface IGroupMemberRepo
{
    // Commands
    Task<Result> ToAdminsAsync(EntityId groupId, List<EntityId> memberIds, EntityId adminModifierId, AdminAccessLevels accessLevel);
    Task<Result> ToAdminAsync(EntityId groupId, EntityId memberId, EntityId adminModifierId, AdminAccessLevels accessLevel);


    Task<Result> ToMembersAsync(EntityId groupId , List<EntityId> memberIds);
    Task<Result> ToMemberAsync(EntityId groupId , EntityId memberId);

    // Queries
    Task<Result<List<AppUser>>> GetMembersAsync(EntityId groupId);
    Task<Result<GroupTbl>> GetGroupsAsync(EntityId userId);
    Task<Result<List<AppUser>>> GetAdminMembersAsync(EntityId groupId);



}
