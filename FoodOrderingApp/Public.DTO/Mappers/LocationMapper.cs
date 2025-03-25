using AutoMapper;
using Base.DAL;

namespace Public.DTO.Mappers;

public class LocationMapper(IMapper mapper) : BaseMapper<App.BLL.DTO.Location, v1.Location>(mapper);

public class LocationRequestMapper(IMapper mapper) : BaseMapper<App.BLL.DTO.Location, v1.LocationRequest>(mapper);