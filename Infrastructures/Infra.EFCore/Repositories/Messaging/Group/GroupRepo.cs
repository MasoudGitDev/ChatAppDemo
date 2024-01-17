using Domains.Messaging.GroupEntity.Repo;
using Domains.Messaging.GroupRequestEntity.Repos;

namespace Infra.EFCore.Repositories.Messaging.Group {
    internal class GroupRepo(IGroupQueries queries , IGroupCommands commands , IGroupRequestRepo groupRequestRepo) : IGroupRepo {
        public IGroupQueries Queries => queries;
        public IGroupCommands Commands => commands;
        public IGroupRequestRepo GroupRequestRepo => groupRequestRepo;
    }
}
