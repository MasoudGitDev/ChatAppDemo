using Domains.Messaging.GroupRequestEntity.Repos;

namespace Infra.EFCore.Repositories.Messaging.GroupRequests;
internal class GroupRequestRepo(
    IGroupRequestCommands commands ,
    IGroupRequestQueries queries) : IGroupRequestRepo
{
    public IGroupRequestCommands Commands => commands;

    public IGroupRequestQueries Queries => queries;
}
