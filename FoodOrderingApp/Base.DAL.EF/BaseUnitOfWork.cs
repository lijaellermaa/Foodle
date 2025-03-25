using Base.Contracts.DAL;
using Microsoft.EntityFrameworkCore;

namespace Base.DAL.EF;

public abstract class BaseUnitOfWork<TAppDbContext>(TAppDbContext dbContext) : IUnitOfWork
    where TAppDbContext : DbContext
{
    protected readonly TAppDbContext UowDbContext = dbContext;

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await UowDbContext.SaveChangesAsync(cancellationToken);
    }

    private readonly Dictionary<Type, object> _repoCache = new();

    protected TRepository GetRepository<TRepository>(Func<TRepository> repoCreationMethod) where TRepository : class
    {
        if (_repoCache.TryGetValue(typeof(TRepository), out var repo))
        {
            return (TRepository)repo;
        }

        var newRepo = repoCreationMethod();
        _repoCache.Add(typeof(TRepository), newRepo);
        return newRepo;
    }
}