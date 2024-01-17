namespace Domains.Messaging.GroupMessageEntity.Repos;  
public interface IGroupMessageRepo {
    public IGroupMessageCommands Commands { get; }
    public IGroupMessageQueries Queries { get; }
}
