namespace Domains.Messaging.GroupEntity.Repo;
public interface IGroupRepo {
    IGroupQueries Queries { get; }
    IGroupCommands Commands { get; }
}
