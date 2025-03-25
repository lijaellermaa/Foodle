using Base.Contracts.DAL.Repositories;
using Base.Contracts.Domain;
using Helpers.Base.Filter;

namespace Base.Contracts.BLL;

public interface IEntityService<TEntity> : IBaseRepository<TEntity>, IEntityService<Guid, TEntity>
    where TEntity : class, IDomainEntityId
{

}

public interface IEntityService<TKey, TEntity> : IBaseRepository<TKey, TEntity>
    where TEntity : class, IDomainEntityId<TKey>
    where TKey : struct, IEquatable<TKey>
{
    Task<IEnumerable<TEntity>> GetAllFilteredAsync(IFilter<TEntity> filter, TKey? userId = default, bool noTracking = false);
}