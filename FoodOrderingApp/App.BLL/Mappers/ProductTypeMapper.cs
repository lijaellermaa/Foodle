using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class ProductTypeMapper : BaseMapper<DTO.ProductType, App.DAL.DTO.ProductType>
{
    public ProductTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}