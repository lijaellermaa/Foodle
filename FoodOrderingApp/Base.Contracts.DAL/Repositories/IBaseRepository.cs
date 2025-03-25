using Base.Contracts.Domain;

namespace Base.Contracts.DAL.Repositories;

public interface IBaseRepository<TEntity> : IBaseRepository<Guid, TEntity>
    where TEntity : class, IDomainEntityId<Guid>
{
    
}

public interface IBaseRepository<TKey, TEntity> : IBaseRepositoryCommon<TKey, TEntity>,
    IBaseRepositoryAsync<TKey, TEntity>
    where TKey : struct, IEquatable<TKey>
    where TEntity : class, IDomainEntityId<TKey>
{
    
}