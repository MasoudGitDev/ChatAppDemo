using Domains.Messaging.GroupMessageEntity.Repos;

namespace Infra.EFCore.Repositories.Messaging.GroupMessages;
internal class GroupMessageRepo(
    IGroupMessageCommands commands ,
    IGroupMessageQueries queries) : IGroupMessageRepo
{
    public IGroupMessageCommands Commands => commands;

    public IGroupMessageQueries Queries => queries;
}
