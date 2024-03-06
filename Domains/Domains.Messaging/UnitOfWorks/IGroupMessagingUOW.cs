using Domains.Messaging.GroupMemberEntity.Repos;
using Domains.Messaging.GroupMessageEntity.Repos;
using Domains.Messaging.GroupRequestEntity.Repos;
using Shared.Generics;

namespace Domains.Messaging.UnitOfWorks;

/// <summary>
/// IGroupMessagingUnitOfWork
/// </summary>
public interface IGroupMessagingUOW {

    public IGroupMemberQueries MemberQueries { get; }
    public IGroupRequestQueries RequestQueries { get; }
    public IGroupMessageQueries MessageQueries { get; }

    public Task SaveChangesAsync();
    public void Remove<T>(T entity) where T : Entity;
    public void RemoveRange<T>(List<T> entity) where T : Entity;
    public Task CreateAsync<T>(T entity) where T : Entity;
}
