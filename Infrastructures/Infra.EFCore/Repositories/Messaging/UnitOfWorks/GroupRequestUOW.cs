using Domains.Messaging.GroupRequestEntity.Repos;
using Domains.Messaging.UnitOfWorks;
using Infra.EFCore.Contexts;
using Infra.EFCore.Exceptions;

namespace Infra.EFCore.Repositories.Messaging.UnitOfWorks;
internal class GroupRequestUOW(
    AppDbContext _appDbContext ,
    IGroupRequestCommands _commands ,
    IGroupRequestQueries _queries
    ) : IGroupRequestUOW {
    public IGroupRequestCommands Commands => _commands;

    public IGroupRequestQueries Queries => _queries;

    [ConcurrencyTryCatch]
    public async Task SaveChangesAsync() => await _appDbContext.SaveChangesAsync();
}
