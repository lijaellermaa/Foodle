using AutoMapper;

namespace Public.DTO;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<App.BLL.DTO.Location, v1.Location>().ReverseMap();
        CreateMap<App.BLL.DTO.OrderItem, v1.OrderItem>().ReverseMap();
        CreateMap<App.BLL.DTO.Order, v1.Order>().ReverseMap();
        CreateMap<App.BLL.DTO.Price, v1.Price>().ReverseMap();
        CreateMap<App.BLL.DTO.Product, v1.Product>().ReverseMap();
        CreateMap<App.BLL.DTO.ProductType, v1.ProductType>().ReverseMap();
        CreateMap<App.BLL.DTO.Restaurant, v1.Restaurant>().ReverseMap();

        CreateMap<App.BLL.DTO.Location, v1.LocationRequest>().ReverseMap();
        CreateMap<App.BLL.DTO.OrderItem, v1.OrderItemRequest>().ReverseMap();
        CreateMap<App.BLL.DTO.Order, v1.OrderRequest>().ReverseMap();
        CreateMap<App.BLL.DTO.Price, v1.PriceRequest>().ReverseMap();
        CreateMap<App.BLL.DTO.Product, v1.ProductRequest>().ReverseMap();
        CreateMap<App.BLL.DTO.ProductType, v1.ProductTypeRequest>().ReverseMap();
        CreateMap<App.BLL.DTO.Restaurant, v1.RestaurantRequest>().ReverseMap();

        CreateMap<App.BLL.DTO.Order, v1.OrderPure>().ReverseMap();
        CreateMap<App.BLL.DTO.Product, v1.ProductPure>().ReverseMap();
        CreateMap<App.BLL.DTO.Price, v1.PricePure>().ReverseMap();
        CreateMap<App.BLL.DTO.Restaurant, v1.RestaurantPure>().ReverseMap();

        // CreateMap<App.BLL.DTO.Identity.AppUser, v1.Identity.AppUser>().ReverseMap();
        // CreateMap<App.BLL.DTO.Identity.AppRole, App.Domain.Identity.AppRole>().ReverseMap();
        // CreateMap<App.BLL.DTO.Identity.AppRefreshToken, App.Domain.Identity.AppRefreshToken>().ReverseMap();
        // CreateMap<App.BLL.DTO.Identity.AppUserRole, App.Domain.Identity.AppUserRole>().ReverseMap();
    }
}