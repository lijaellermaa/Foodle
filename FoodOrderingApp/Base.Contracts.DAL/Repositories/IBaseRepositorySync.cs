using Base.Contracts.Domain;

namespace Base.Contracts.DAL.Repositories;

public interface IBaseRepositorySync<TKey, TEntity>
    where TKey : struct, IEquatable<TKey>
    where TEntity : class, IDomainEntityId<TKey>
{
    TEntity FirstOrDefault(TKey id, TKey? userId = default, bool noTracking = true);
    IEnumerable<TEntity> GetAll(TKey? userId = default, bool noTracking = true);
    bool Exists(TKey id, TKey? userId = default);
    TEntity Remove(TKey id, TKey? userId = default);
}