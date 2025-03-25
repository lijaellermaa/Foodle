using AutoMapper;
using Base.DAL;

namespace App.DAL.DTO.Mappers;

public class ProductTypeMapper : BaseMapper<App.Domain.ProductType, ProductType>
{
    public ProductTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}