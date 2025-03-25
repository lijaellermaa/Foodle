using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class RestaurantMapper : BaseMapper<DTO.Restaurant, App.DAL.DTO.Restaurant>
{
    public RestaurantMapper(IMapper mapper) : base(mapper)
    {
    }
}