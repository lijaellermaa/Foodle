using Base.Contracts.Domain;

namespace Base.Contracts.DAL.Repositories;

public interface IBaseRepositoryCommon<TKey, TEntity>
    where TKey : struct, IEquatable<TKey>
    where TEntity : class, IDomainEntityId<TKey>
{
    TEntity Add(TEntity entity);
    TEntity? Update(TEntity entity);
    TEntity Remove(TEntity entity, TKey? userId = default);
}