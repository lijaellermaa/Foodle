using Base.Contracts.BLL;
using Base.Contracts.DAL;

namespace Base.BLL;

public abstract class BaseBll<TUow>(TUow uow) : IBaseBll
    where TUow : IUnitOfWork
{
    private readonly TUow _uow = uow;

    public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _uow.SaveChangesAsync(cancellationToken);
    }
}