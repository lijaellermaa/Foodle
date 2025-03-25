using AutoMapper;

namespace App.DAL.DTO;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<Location, App.Domain.Location>().ReverseMap();
        CreateMap<OrderItem, App.Domain.OrderItem>().ReverseMap();
        CreateMap<Order, App.Domain.Order>().ReverseMap();
        CreateMap<Price, App.Domain.Price>().ReverseMap();
        CreateMap<Product, App.Domain.Product>().ReverseMap();
        CreateMap<ProductType, App.Domain.ProductType>().ReverseMap();
        CreateMap<Restaurant, App.Domain.Restaurant>().ReverseMap();
        CreateMap<Identity.AppUser, App.Domain.Identity.AppUser>().ReverseMap();
        CreateMap<Identity.AppRole, App.Domain.Identity.AppRole>().ReverseMap();
        CreateMap<Identity.AppRefreshToken, App.Domain.Identity.AppRefreshToken>().ReverseMap();
        CreateMap<Identity.AppUserRole, App.Domain.Identity.AppUserRole>().ReverseMap();
    }
}