using App.BLL.DTO;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IOrderItemService : IEntityService<OrderItem>
{
    
    Task<List<OrderItem?>?> RemoveByOrderIdAsync(Guid orderId);
    
}