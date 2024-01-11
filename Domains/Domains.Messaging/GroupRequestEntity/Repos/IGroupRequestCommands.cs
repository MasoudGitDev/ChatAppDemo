namespace Domains.Messaging.GroupRequestEntity.Repos;  
public interface IGroupRequestCommands {
    Task CreateAsync(GroupRequestTbl groupRequest);
    Task DeleteAsync(GroupRequestTbl groupRequest);
    Task UpdateAsync(GroupRequestTbl groupRequest);
}
