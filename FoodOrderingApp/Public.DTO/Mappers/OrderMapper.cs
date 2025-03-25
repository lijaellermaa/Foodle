using AutoMapper;
using Base.DAL;

namespace Public.DTO.Mappers;

public class OrderMapper(IMapper mapper) : BaseMapper<App.BLL.DTO.Order, v1.Order>(mapper);

public class OrderRequestMapper(IMapper mapper) : BaseMapper<App.BLL.DTO.Order, v1.OrderRequest>(mapper);