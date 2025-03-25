using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.DAL.DTO.Mappers;
using App.DAL.EF.Repositories;
using AutoMapper;
using Base.Contracts;
using Base.DAL.EF;

namespace App.DAL.EF;

public class AppUOW : BaseUnitOfWork<AppDbContext>, IAppUnitOfWork
{
    private readonly IMapper _mapper;
    
    public AppUOW(AppDbContext dbContext, IMapper mapper) : base(dbContext)
    {
        _mapper = mapper;
    }
    
    public ILocationRepository LocationRepository => GetRepository(() => new LocationRepository(UowDbContext, new LocationMapper(_mapper)));
    public IOrderItemRepository OrderItemRepository => GetRepository(() => new OrderItemRepository(UowDbContext, new OrderItemMapper(_mapper)));
    public IOrderRepository OrderRepository => GetRepository(() => new OrderRepository(UowDbContext, new OrderMapper(_mapper)));
    public IPriceRepository PriceRepository => GetRepository(() => new PriceRepository(UowDbContext, new PriceMapper(_mapper)));
    public IProductRepository ProductRepository => GetRepository(() => new ProductRepository(UowDbContext, new ProductMapper(_mapper)));
    public IProductTypeRepository ProductTypeRepository => GetRepository(() => new ProductTypeRepository(UowDbContext, new ProductTypeMapper(_mapper)));
    public IRestaurantRepository RestaurantRepository => GetRepository(() => new RestaurantRepository(UowDbContext, new RestaurantMapper(_mapper)));
}