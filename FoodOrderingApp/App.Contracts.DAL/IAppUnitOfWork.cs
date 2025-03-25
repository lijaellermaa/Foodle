using App.Contracts.DAL.Repositories;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IAppUnitOfWork : IUnitOfWork
{
    ILocationRepository LocationRepository { get; }
    IOrderItemRepository OrderItemRepository { get; }
    IOrderRepository OrderRepository { get; }
    IPriceRepository PriceRepository { get; }
    IProductRepository ProductRepository { get; }
    IProductTypeRepository ProductTypeRepository { get; }
    IRestaurantRepository RestaurantRepository { get; }
}