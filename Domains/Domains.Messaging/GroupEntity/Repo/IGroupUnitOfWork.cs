namespace Domains.Messaging.GroupEntity.Repo;  
public interface IGroupUnitOfWork {
    public IGroupRepo GroupRepo { get;}
    public IUpdateGroupRepo UpdateRepo { get;}

}
