using DAL.Contracts.Base.Repositories;
using Domain.App;

namespace DAL.Contracts.App.Repositories;

public interface IOrderRepository : IBaseRepository<Order>
{
    Task<IEnumerable<Order>> AllAsync(Guid userId);
    Task<Order?> FindAsync(Guid id, Guid userId);
    Task<Order?> RemoveAsync(Guid id, Guid userId);
    Task<Order?> FirstOrDefaultAsync(Guid id, Guid userId);
    Task<bool> ExistsAsync(Guid id);
    Task<bool> IsOwnedByUserAsync(Guid id, Guid userId);
    
    Task<Order?> FindByAppUserIdAndCompleted(Guid userId, bool completed);
}