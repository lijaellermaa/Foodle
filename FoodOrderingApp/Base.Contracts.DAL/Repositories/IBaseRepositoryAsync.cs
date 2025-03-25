using Base.Contracts.Domain;

namespace Base.Contracts.DAL.Repositories;

public interface IBaseRepositoryAsync<TKey, TEntity>
    where TKey : struct, IEquatable<TKey>
    where TEntity : class, IDomainEntityId<TKey>
{
    Task<IEnumerable<TEntity>> GetAllAsync(TKey? userId = default, bool noTracking = false);
    Task<TEntity?> FirstOrDefaultAsync(TKey id, TKey? userId = default, bool noTracking = false);
    Task<bool> ExistsAsync(TKey id, TKey? userId = default);
    Task<TEntity?> RemoveAsync(TKey id, TKey? userId = default);
}