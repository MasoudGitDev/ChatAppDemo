using Domains.Messaging.GroupRequestEntity.Repos;

namespace Domains.Messaging.UnitOfWorks;

/// <summary>
/// IGroupRequestUnitOfWork
/// </summary>
public interface IGroupRequestUOW {
    public IGroupRequestCommands Commands { get; }
    public IGroupRequestQueries Queries { get; }
    public Task SaveChangesAsync();
}
