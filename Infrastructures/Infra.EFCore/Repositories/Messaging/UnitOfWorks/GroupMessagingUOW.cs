using Domains.Messaging.GroupMemberEntity.Repos;
using Domains.Messaging.GroupMessageEntity.Repos;
using Domains.Messaging.GroupRequestEntity.Repos;
using Domains.Messaging.UnitOfWorks;
using Infra.EFCore.Contexts;
using Infra.EFCore.Exceptions;
using Shared.Generics;

namespace Infra.EFCore.Repositories.Messaging.UnitOfWorks;
internal class GroupMessagingUOW(
    AppDbContext _appDbContext ,
    IGroupMemberQueries memberQueries ,
    IGroupRequestQueries requestQueries ,
    IGroupMessageQueries messageQueries
    ) : IGroupMessagingUOW {

    public IGroupMemberQueries MemberQueries { get => memberQueries; }
    public IGroupRequestQueries RequestQueries { get => requestQueries; }
    public IGroupMessageQueries MessageQueries { get => messageQueries; }

    [ConcurrencyTryCatch]
    public async Task SaveChangesAsync()
        => await _appDbContext.SaveChangesAsync();

    public void Remove<T>(T entity) where T : Entity {
        _appDbContext.Set<T>().Remove(entity);
    }
    public void RemoveRange<T>(List<T> entities) where T : Entity {
        _appDbContext.Set<T>().RemoveRange(entities);
    }

    public async Task CreateAsync<T>(T entity) where T : Entity {
        await _appDbContext.AddAsync(entity);
    }
}
