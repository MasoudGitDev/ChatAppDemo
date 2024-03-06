using Domains.Messaging.GroupMessageEntity.Aggregate;

namespace Domains.Messaging.GroupMessageEntity.Repos;
public interface IGroupMessageCommands {
    Task CreateAsync(GroupMessageTbl message);
    Task UpdateAsync(GroupMessageTbl message);
    Task DeleteAsync(GroupMessageTbl message);
}
