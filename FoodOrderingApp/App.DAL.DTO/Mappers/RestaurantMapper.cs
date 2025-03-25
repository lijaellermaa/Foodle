using AutoMapper;
using Base.DAL;

namespace App.DAL.DTO.Mappers;

public class RestaurantMapper : BaseMapper<App.Domain.Restaurant, Restaurant>
{
    public RestaurantMapper(IMapper mapper) : base(mapper)
    {
    }
}