using Domains.Messaging.GroupRequestEntity.Repos;

namespace Infra.EFCore.Repositories.Messaging.Group.Requests;
internal class GroupRequestRepo(IGroupRequestCommands commands , IGroupRequestQueries queries) : IGroupRequestRepo {
    public IGroupRequestCommands Commands => commands;

    public IGroupRequestQueries Queries => queries;
}
