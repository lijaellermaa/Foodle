using App.Domain;
using Base.Contracts.DAL.Repositories;
using Helpers.Base.Entities;
using Order=App.DAL.DTO.Order;

namespace App.Contracts.DAL.Repositories;

public interface IOrderRepository : IBaseRepository<Order>
{
    Task<bool> IsOwnedByUserAsync(Guid id, Guid userId);
    Task<Order?> FindByUserAndStatusAsync(Guid userId, OrderStatus status, bool noTracking = true);
}