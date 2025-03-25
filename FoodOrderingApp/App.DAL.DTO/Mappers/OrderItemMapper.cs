using AutoMapper;
using Base.DAL;

namespace App.DAL.DTO.Mappers;

public class OrderItemMapper : BaseMapper<App.Domain.OrderItem, OrderItem>
{
    public OrderItemMapper(IMapper mapper) : base(mapper)
    {
    }
}