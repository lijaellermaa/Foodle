using AutoMapper;
using Base.DAL;

namespace Public.DTO.Mappers;

public class RestaurantMapper(IMapper mapper) : BaseMapper<App.BLL.DTO.Restaurant, v1.Restaurant>(mapper);

public class RestaurantRequestMapper(IMapper mapper) : BaseMapper<App.BLL.DTO.Restaurant, v1.RestaurantRequest>(mapper);