using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class OrderMapper : BaseMapper<DTO.Order, App.DAL.DTO.Order>
{
    public OrderMapper(IMapper mapper) : base(mapper)
    {
    }
}