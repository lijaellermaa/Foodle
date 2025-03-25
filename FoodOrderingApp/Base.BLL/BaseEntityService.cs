using Base.Contracts;
using Base.Contracts.BLL;
using Base.Contracts.DAL.Repositories;
using Base.Contracts.Domain;
using Helpers.Base.Extensions;
using Helpers.Base.Filter;

namespace Base.BLL;

public class BaseEntityService<TBllEntity, TDalEntity, TRepository>(TRepository repository, IMapper<TBllEntity, TDalEntity> mapper)
    : BaseEntityService<TBllEntity, TDalEntity, TRepository, Guid>(repository, mapper), IEntityService<TBllEntity>
    where TBllEntity : class, IDomainEntityId
    where TDalEntity : class, IDomainEntityId
    where TRepository : IBaseRepository<TDalEntity>;

public class BaseEntityService<TBllEntity, TDalEntity, TRepository, TKey>(TRepository repository, IMapper<TBllEntity, TDalEntity> mapper)
    : IEntityService<TKey, TBllEntity>
    where TBllEntity : class, IDomainEntityId<TKey>
    where TDalEntity : class, IDomainEntityId<TKey>
    where TRepository : IBaseRepository<TKey, TDalEntity>
    where TKey : struct, IEquatable<TKey>
{
    protected readonly TRepository _repository = repository;
    protected readonly IMapper<TBllEntity, TDalEntity> Mapper = mapper;

    public TBllEntity Add(TBllEntity entity)
    {
        var dalEntity = Mapper.Map(entity)!;
        var updatedDalEntity = _repository.Add(dalEntity);
        var bllEntity = Mapper.Map(updatedDalEntity)!;

        return bllEntity;
    }

    public TBllEntity Update(TBllEntity entity)
    {
        return Mapper.Map(_repository.Update(Mapper.Map(entity)!))!;
    }

    public TBllEntity Remove(TBllEntity entity, TKey? userId = default)
    {
        return Mapper.Map(_repository.Remove(Mapper.Map(entity)!, userId))!;
    }

    public async Task<IEnumerable<TBllEntity>> GetAllAsync(TKey? userId = default, bool noTracking = false)
    {
        var entities = await _repository.GetAllAsync(userId, noTracking);
        return entities.Select(Mapper.Map)!;
    }

    public async Task<IEnumerable<TBllEntity>> GetAllFilteredAsync(IFilter<TBllEntity> filter, TKey? userId = default, bool noTracking = false)
    {
        var entities = await _repository
            .GetAllAsync(userId, noTracking);
        return entities
            .Select(x => Mapper.Map(x)!)
            .Filter(filter);
    }

    public async Task<TBllEntity?> FirstOrDefaultAsync(TKey id, TKey? userId = default, bool noTracking = false)
    {
        return Mapper.Map(await _repository.FirstOrDefaultAsync(id, userId, noTracking));
    }

    public async Task<bool> ExistsAsync(TKey id, TKey? userId = default)
    {
        return await _repository.ExistsAsync(id, userId);
    }

    public async Task<TBllEntity?> RemoveAsync(TKey id, TKey? userId = default)
    {
        return Mapper.Map(await _repository.RemoveAsync(id, userId));
    }
}