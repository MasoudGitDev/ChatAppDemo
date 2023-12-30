using Shared.Models;
using Shared.ValueObjects;

namespace Domains.Messaging.GroupRequesterEntity.Repos;  
public interface IGroupRequesterRepo{
    Task CreateAsync(GroupRequesterTbl entity);
    Task<GroupRequesterTbl?> GetAsync(EntityId groupId , EntityId requesterId);
    Task UpdateAsync(GroupRequesterTbl entity);
}
