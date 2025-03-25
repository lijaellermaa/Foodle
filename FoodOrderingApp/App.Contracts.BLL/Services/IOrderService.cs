using App.BLL.DTO;
using Base.Contracts.BLL;
using Helpers.Base.Entities;

namespace App.Contracts.BLL.Services;

public interface IOrderService : IEntityService<Order>
{
    Task<bool> IsOwnedByUserAsync(Guid id, Guid userId);
    Task<Order?> FindByUserAndStatusAsync(Guid userId, OrderStatus status);
    Task<bool> PayOrderAsync(Guid id, Guid userId);
}