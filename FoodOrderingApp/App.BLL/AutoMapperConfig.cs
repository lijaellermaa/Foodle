using AutoMapper;

namespace App.BLL;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<App.BLL.DTO.Location, App.DAL.DTO.Location>().ReverseMap();
        CreateMap<App.BLL.DTO.OrderItem, App.DAL.DTO.OrderItem>().ReverseMap();
        CreateMap<App.BLL.DTO.Order, App.DAL.DTO.Order>().ReverseMap();
        CreateMap<App.BLL.DTO.Price, App.DAL.DTO.Price>().ReverseMap();
        CreateMap<App.BLL.DTO.Product, App.DAL.DTO.Product>().ReverseMap();
        CreateMap<App.BLL.DTO.ProductType, App.DAL.DTO.ProductType>().ReverseMap();
        CreateMap<App.BLL.DTO.Restaurant, App.DAL.DTO.Restaurant>().ReverseMap();
        CreateMap<App.BLL.DTO.Identity.AppUser, App.DAL.DTO.Identity.AppUser>().ReverseMap();
        CreateMap<App.BLL.DTO.Identity.AppRole, App.DAL.DTO.Identity.AppRole>().ReverseMap();
        CreateMap<App.BLL.DTO.Identity.AppRefreshToken, App.DAL.DTO.Identity.AppRefreshToken>().ReverseMap();
        CreateMap<App.BLL.DTO.Identity.AppUserRole, App.DAL.DTO.Identity.AppUserRole>().ReverseMap();
    }
}