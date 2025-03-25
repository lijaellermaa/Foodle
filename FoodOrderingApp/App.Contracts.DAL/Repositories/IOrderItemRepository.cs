using App.DAL.DTO;
using Base.Contracts.DAL.Repositories;

namespace App.Contracts.DAL.Repositories;

public interface IOrderItemRepository : IBaseRepository<OrderItem>
{
    Task<List<OrderItem?>?> RemoveByOrderIdAsync(Guid orderId, Guid? userId = default);
}