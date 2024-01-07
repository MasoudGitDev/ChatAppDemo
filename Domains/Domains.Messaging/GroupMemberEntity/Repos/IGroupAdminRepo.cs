using Domains.Messaging.GroupEntity.Repo;

namespace Domains.Messaging.GroupMemberEntity.Repos;
public interface IGroupAdminRepo
{
    IGroupAdminCommands Commands { get; }
    IGroupAdminQueries Queries { get; }
    IGroupRepo General { get; }
}
