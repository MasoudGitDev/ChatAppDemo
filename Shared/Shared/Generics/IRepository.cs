using Shared.Models;
using Shared.ValueObjects;

namespace Shared.Generics;

public interface IUpdateRepository<T> where T:class , IEntity {
    Task<Result> UpdateAsync(T entity);
}
public interface IRepository<T> where T : class , IEntity {
    Task<Result> CreateAsync(T entity);
    Task<Result> DeleteAsync(EntityId entityId);
    Task<Result<T>> GetAsync(EntityId entityId);
}

