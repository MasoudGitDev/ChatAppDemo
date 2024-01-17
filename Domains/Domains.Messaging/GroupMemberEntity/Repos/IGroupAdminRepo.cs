using Domains.Messaging.GroupEntity.Repo;
using Domains.Messaging.GroupMessageEntity.Repos;
using Domains.Messaging.GroupRequestEntity.Repos;

namespace Domains.Messaging.GroupMemberEntity.Repos;
public interface IGroupAdminRepo
{
    IGroupAdminCommands Commands { get; }
    IGroupAdminQueries Queries { get; }
    IGroupRepo General { get; }
    IGroupRequestRepo RequestRepo { get; }
    IGroupMessageRepo MessageRepo { get; }
}
