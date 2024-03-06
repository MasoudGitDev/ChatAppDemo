using Domains.Messaging.GroupMemberEntity.Repos;

namespace Infra.EFCore.Repositories.Messaging.GroupMembers;
internal class GroupAdminRepo(
    IGroupAdminCommands commands,
    IGroupMemberQueries queries) : IGroupAdminRepo
{
    public IGroupAdminCommands Commands => commands;

    public IGroupMemberQueries Queries => queries;
}
