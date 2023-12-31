using Shared.Models;
using Shared.ValueObjects;

namespace Domains.Messaging.GroupRequestEntity.Repos;  
public interface IGroupRequesterRepo{
    Task CreateAsync(GroupRequestTbl entity);
    Task<GroupRequestTbl?> GetAsync(EntityId groupId , EntityId requesterId);
    Task UpdateAsync(GroupRequestTbl entity);
    Task RemoveAsync(GroupRequestTbl entity);
}
