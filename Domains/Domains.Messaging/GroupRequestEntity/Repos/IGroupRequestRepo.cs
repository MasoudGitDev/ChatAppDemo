namespace Domains.Messaging.GroupRequestEntity.Repos;  
public interface IGroupRequestRepo {
    public IGroupRequestCommands Commands { get;}
    public IGroupRequestQueries Queries { get; }
}
