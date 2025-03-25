using App.Contracts.BLL.Services;
using Base.Contracts.BLL;

namespace App.Contracts.BLL;

public interface IAppBll : IBaseBll
{
    ILocationService LocationService { get; }
    IOrderService OrderService { get; }
    IOrderItemService OrderItemService { get; }
    IPriceService PriceService { get; }
    IProductService ProductService { get; }
    IProductTypeService ProductTypeService { get; }
    IRestaurantService RestaurantService { get; }
}