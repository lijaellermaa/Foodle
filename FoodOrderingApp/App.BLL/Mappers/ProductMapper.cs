using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class ProductMapper : BaseMapper<DTO.Product, App.DAL.DTO.Product>
{
    public ProductMapper(IMapper mapper) : base(mapper)
    {
    }
}