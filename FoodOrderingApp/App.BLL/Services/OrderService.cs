using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using Base.BLL;
using Base.Contracts;
using Helpers.Base.Entities;

namespace App.BLL.Services;

public class OrderService(IAppUnitOfWork uow, IMapper<Order, DAL.DTO.Order> mapper) : BaseEntityService<Order, DAL.DTO.Order, IOrderRepository>(uow.OrderRepository, mapper), IOrderService
{
    public async Task<bool> IsOwnedByUserAsync(Guid id, Guid userId)
    {
        return await uow.OrderRepository.IsOwnedByUserAsync(id, userId);
    }

    public async Task<Order?> FindByUserAndStatusAsync(Guid userId, OrderStatus status)
    {
        return Mapper.Map(await uow.OrderRepository.FindByUserAndStatusAsync(userId, status));
    }
    public async Task<bool> PayOrderAsync(Guid id, Guid userId)
    {
        var order = await uow.OrderRepository.FirstOrDefaultAsync(id);
        if (order == null)
        {
            return false;
        }

        if (order.AppUserId != userId)
        {
            return false;
        }

        if (order.Status != OrderStatus.InDelivery)
        {
            return false;
        }
        order.Status = OrderStatus.Completed;
        uow.OrderRepository.Update(order);
        await uow.SaveChangesAsync();
        return true;
    }
}