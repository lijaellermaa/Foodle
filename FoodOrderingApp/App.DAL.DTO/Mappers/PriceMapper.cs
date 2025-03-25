using AutoMapper;
using Base.DAL;

namespace App.DAL.DTO.Mappers;

public class PriceMapper : BaseMapper<App.Domain.Price, Price>
{
    public PriceMapper(IMapper mapper) : base(mapper)
    {
    }
}