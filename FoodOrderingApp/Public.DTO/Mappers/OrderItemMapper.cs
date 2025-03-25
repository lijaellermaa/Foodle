using AutoMapper;
using Base.DAL;

namespace Public.DTO.Mappers;

public class OrderItemMapper(IMapper mapper) : BaseMapper<App.BLL.DTO.OrderItem, v1.OrderItem>(mapper);

public class OrderItemRequestMapper(IMapper mapper) : BaseMapper<App.BLL.DTO.OrderItem, v1.OrderItemRequest>(mapper);