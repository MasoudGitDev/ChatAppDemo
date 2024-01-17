using Domains.Messaging.GroupRequestEntity.Repos;

namespace Domains.Messaging.GroupEntity.Repo;
public interface IGroupRepo {
    IGroupQueries Queries { get; }
    IGroupCommands Commands { get; }
    IGroupRequestRepo GroupRequestRepo { get; }
}
