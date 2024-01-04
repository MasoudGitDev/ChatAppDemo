using Domains.Messaging.GroupEntity.Repo;

namespace Infra.EFCore.Repositories.Messaging.Group {
    internal class GroupRepo(IGroupQueries queries , IGroupCommands commands) : IGroupRepo {
        public IGroupQueries Queries => queries;
        public IGroupCommands Commands => commands;
    }
}
