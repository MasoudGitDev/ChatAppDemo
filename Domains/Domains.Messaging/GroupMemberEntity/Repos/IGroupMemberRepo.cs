using Domains.Messaging.GroupEntity;
using Domains.Messaging.Shared.Models;
using Shared.ValueObjects;

namespace Domains.Messaging.GroupMemberEntity.Repos;
public interface IGroupMemberRepo
{
    Task RemoveMemberAsync(GroupMemberTbl entity);

    Task<GroupMemberTbl?> GetMemberAsync(EntityId groupId , EntityId memberId);
    Task<List<MemberInfo>> GetMembersAsync(EntityId groupId);
    Task<List<GroupInfo>> GetGroupsAsync(EntityId userId);
    
}
