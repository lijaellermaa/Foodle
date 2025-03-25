using AutoMapper;
using Base.DAL;

namespace App.DAL.DTO.Mappers;

public class ProductMapper : BaseMapper<App.Domain.Product, Product>
{
    public ProductMapper(IMapper mapper) : base(mapper)
    {
    }
}