namespace Domains.Messaging.GroupRequestEntity.Repos;  
public interface IGroupRequestCommands {
    Task CreateAsync(GroupRequestTbl groupRequest);
    void Delete(GroupRequestTbl groupRequest);
    void Update(GroupRequestTbl groupRequest);
}
