using System.Security.Authentication;
using Base.Contracts;
using Base.Contracts.DAL.Repositories;
using Base.Contracts.Domain;
using Microsoft.EntityFrameworkCore;

namespace Base.DAL.EF;

public class BaseEntityRepository<TDomainEntity, TDalEntity, TDbContext>(
    TDbContext dataContext,
    IMapper<TDomainEntity, TDalEntity> mapper
) : BaseEntityRepository<Guid, TDomainEntity, TDalEntity, TDbContext>(dataContext,
mapper)
    where TDomainEntity : class, IDomainEntityId<Guid>
    where TDalEntity : class, IDomainEntityId<Guid>
    where TDbContext : DbContext;

public class BaseEntityRepository<TKey, TDomainEntity, TDalEntity, TDbContext> : IBaseRepository<TKey, TDalEntity>
    where TDalEntity : class, IDomainEntityId<TKey>
    where TDomainEntity : class, IDomainEntityId<TKey>
    where TKey : struct, IEquatable<TKey>
    where TDbContext : DbContext
{
    protected readonly TDbContext RepositoryDbContext;
    protected readonly DbSet<TDomainEntity> RepositoryDbSet;
    protected readonly IMapper<TDomainEntity, TDalEntity> Mapper;
    private readonly Dictionary<TDalEntity, TDomainEntity> _entityCache = new Dictionary<TDalEntity, TDomainEntity>();

    protected BaseEntityRepository(TDbContext dataContext, IMapper<TDomainEntity, TDalEntity> mapper)
    {
        RepositoryDbContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        RepositoryDbSet = RepositoryDbContext.Set<TDomainEntity>();
        Mapper = mapper;
    }

    protected virtual IQueryable<TDomainEntity> LoadProperties(IQueryable<TDomainEntity> query)
    {
        return RepositoryDbSet.EntityType.GetNavigations()
            .Aggregate(query, (current, property) => current.Include(property.Name));
    }

    public TDalEntity GetUpdatedEntityAfterSaveChanges(TDalEntity entity)
    {
        var updatedEntity = _entityCache[entity];
        var dalEntity = Mapper.Map(updatedEntity)!;

        return dalEntity;
    }

    public virtual async Task<IEnumerable<TDalEntity>> GetAllAsync(TKey? userId = default, bool noTracking = false)
    {
        var query = CreateQuery(userId, noTracking);
        if (!noTracking) query = LoadProperties(query);
        var resQuery = query.Select(domainEntity => Mapper.Map(domainEntity));
        var res = await resQuery.ToListAsync();

        return res!;
    }

    public virtual async Task<TDalEntity?> FirstOrDefaultAsync(TKey id, TKey? userId = default,
        bool noTracking = false)
    {
        var query = CreateQuery(userId, noTracking);
        if (!noTracking) query = LoadProperties(query);
        return Mapper.Map(await query.FirstOrDefaultAsync(e => e.Id.Equals(id)));
    }

    public virtual TDalEntity Add(TDalEntity entity)
    {
        var domainEntity = Mapper.Map(entity)!;
        var updatedDomainEntity = RepositoryDbSet.Add(domainEntity).Entity;
        var dalEntity = Mapper.Map(updatedDomainEntity)!;

        _entityCache.Add(entity, domainEntity);

        return dalEntity;
    }

    public virtual TDalEntity? Update(TDalEntity entity)
    {
        var domainEntity = Mapper.Map(entity);
        var updatedEntity = RepositoryDbSet.Update(domainEntity!).Entity;
        var dalEntity = Mapper.Map(updatedEntity);

        return dalEntity;
    }

    public virtual TDalEntity Remove(TDalEntity entity, TKey? userId = default)
    {
        var entityToRemove = RepositoryDbSet.FirstOrDefault(e => e.Id.Equals(entity.Id));
        if (userId != null && !userId.Equals(default) &&
            typeof(IDomainEntityAppUser<TKey>).IsAssignableFrom(typeof(TDomainEntity)) &&
            !((IDomainEntityAppUser<TKey>)entity).AppUserId.Equals(userId))
        {
            throw new AuthenticationException(
            $"Bad user id inside entity {typeof(TDalEntity).Name} to be deleted.");
            // TODO: load entity from the db, check that userId inside entity is correct.
        }

        RepositoryDbSet.Remove(entityToRemove);
        return entity;
    }

    public virtual async Task<bool> ExistsAsync(TKey id, TKey? userId = default)
    {
        if (userId == null || userId.Equals(default))
            return await RepositoryDbSet.AnyAsync(e => e.Id.Equals(id));

        if (!typeof(IDomainEntityAppUser<TKey>).IsAssignableFrom(typeof(TDomainEntity)))
            throw new AuthenticationException(
            $"Entity {typeof(TDomainEntity).Name} does not implement required interface: {typeof(IDomainEntityAppUser<TKey>).Name} for AuthorId check");

        return await RepositoryDbSet
            .AnyAsync(e => e.Id.Equals(id) && ((IDomainEntityAppUser<TKey>)e).AppUserId.Equals(userId));
    }

    public virtual async Task<TDalEntity?> RemoveAsync(TKey id, TKey? userId = default)
    {
        var entity = await FirstOrDefaultAsync(id, userId, true);
        if (entity == null)
            throw new NullReferenceException($"Entity {typeof(TDalEntity).Name} with id {id} not found.");
        return Remove(entity, userId);
    }

    protected IQueryable<TDomainEntity> CreateQuery(TKey? userId = default, bool noTracking = false)
    {
        var query = RepositoryDbSet.AsQueryable();
        if (userId != null && !userId.Equals(default) &&
            typeof(IDomainEntityAppUser<TKey>).IsAssignableFrom(typeof(TDomainEntity)))
            query = query.Where(e => ((IDomainEntityAppUser<TKey>)e).AppUserId.Equals(userId));

        return noTracking ? query.AsNoTracking() : query;
    }
}