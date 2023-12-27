using Shared.Models;

namespace Shared.Generics;

public interface IUpdateRepository<T> where T:class , IEntity {
    Task<Result> UpdateAsync(T entity);
}
public interface IGetRepository<T> where T:class , IEntity {
    Task<T?> GetAsync(string entityId);
}

public interface IRepository<T> where T : class , IEntity {
    Task<Result> CreateAsync(T entity);
    Task<Result> DeleteAsync(string entityId);
    Task<Result<T>> GetAsync(string entityId);
}

