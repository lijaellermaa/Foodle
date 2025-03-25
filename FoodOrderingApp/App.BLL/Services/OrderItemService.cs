using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class OrderItemService(IAppUnitOfWork uow, IMapper<OrderItem, DAL.DTO.OrderItem> mapper)
    : BaseEntityService<OrderItem, DAL.DTO.OrderItem, IOrderItemRepository>(uow.OrderItemRepository, mapper), IOrderItemService
{
    public async Task<List<OrderItem?>?> RemoveByOrderIdAsync(Guid orderId)
    {
        return (await _repository.RemoveByOrderIdAsync(orderId))
            .Select(Mapper.Map)
            .ToList();
    }
}