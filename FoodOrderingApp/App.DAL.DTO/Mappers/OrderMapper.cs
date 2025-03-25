using AutoMapper;
using Base.DAL;

namespace App.DAL.DTO.Mappers;

public class OrderMapper : BaseMapper<App.Domain.Order, Order>
{
    public OrderMapper(IMapper mapper) : base(mapper)
    {
    }
}