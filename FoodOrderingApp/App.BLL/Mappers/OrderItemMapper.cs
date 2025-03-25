using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class OrderItemMapper : BaseMapper<DTO.OrderItem, App.DAL.DTO.OrderItem>
{
    public OrderItemMapper(IMapper mapper) : base(mapper)
    {
    }
}