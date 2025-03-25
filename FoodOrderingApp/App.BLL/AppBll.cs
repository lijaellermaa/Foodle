using App.BLL.Mappers;
using App.BLL.Services;
using App.Contracts.BLL;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using AutoMapper;
using Base.BLL;

namespace App.BLL;

public class AppBll : BaseBll<IAppUnitOfWork>, IAppBll
{
    protected IAppUnitOfWork Uow;
    private readonly IMapper _mapper;
    
    public AppBll(IAppUnitOfWork uow, IMapper mapper) : base(uow)
    {
        Uow = uow;
        _mapper = mapper;
    }

    private ILocationService? _locations;
    
    public ILocationService LocationService => 
        _locations ??= new LocationService(Uow, new LocationMapper(_mapper));

    private IOrderService? _orders;
    
    public IOrderService OrderService => 
        _orders ??= new OrderService(Uow, new OrderMapper(_mapper));

    private IOrderItemService? _orderItems;
    
    public IOrderItemService OrderItemService => 
        _orderItems ??= new OrderItemService(Uow, new OrderItemMapper(_mapper));

    private IPriceService? _prices;
    
    public IPriceService PriceService => 
        _prices ??= new PriceService(Uow, new PriceMapper(_mapper));

    private IProductService? _products;
    
    public IProductService ProductService => 
        _products ??= new ProductService(Uow, new ProductMapper(_mapper));

    private IProductTypeService? _productTypes;
    
    public IProductTypeService ProductTypeService => 
        _productTypes ??= new ProductTypeService(Uow, new ProductTypeMapper(_mapper));

    private IRestaurantService? _restaurants;

    public IRestaurantService RestaurantService => 
        _restaurants ??= new RestaurantService(Uow, new RestaurantMapper(_mapper));
}