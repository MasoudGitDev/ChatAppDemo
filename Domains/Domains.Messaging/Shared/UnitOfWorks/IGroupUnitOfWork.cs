using Domains.Messaging.GroupEntity.Repo;
using Domains.Messaging.GroupMemberEntity.Repos;

namespace Domains.Messaging.Shared.UnitOfWorks;  
public interface IGroupUnitOfWork {
    public IGroupRepo GroupRepo { get;}
    public IGroupAdminRepo AdminRepo { get; }
    public IGroupMemberRepo MemberRepo { get;}
}
