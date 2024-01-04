using Domains.Messaging.GroupEntity.Repo;
using Domains.Messaging.GroupMemberEntity.Repos;

namespace Infra.EFCore.Repositories.Messaging.Group;
internal class GroupAdminRepo(IGroupAdminCommands commands , IGroupAdminQueries queries , IGroupRepo general) : IGroupAdminRepo
{
    public IGroupAdminCommands Commands => commands;

    public IGroupAdminQueries Queries => queries;

    public IGroupRepo General => general;
}
