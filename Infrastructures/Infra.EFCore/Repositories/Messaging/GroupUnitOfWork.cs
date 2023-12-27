using Domains.Messaging.GroupEntity.Repo;

namespace Infra.EFCore.Repositories.Messaging {
    internal sealed class GroupUnitOfWork(IGroupRepo groupRepo,IUpdateGroupRepo updateRepo) : IGroupUnitOfWork {
        public IGroupRepo GroupRepo => groupRepo;
        public IUpdateGroupRepo UpdateRepo => updateRepo;
    }
}
