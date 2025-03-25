using AutoMapper;
using Base.DAL;

namespace Public.DTO.Mappers;

public class PriceMapper(IMapper mapper) : BaseMapper<App.BLL.DTO.Price, v1.Price>(mapper);

public class PriceRequestMapper(IMapper mapper) : BaseMapper<App.BLL.DTO.Price, v1.PriceRequest>(mapper);