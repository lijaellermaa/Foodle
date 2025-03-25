using AutoMapper;
using Base.DAL;

namespace Public.DTO.Mappers;

public class ProductTypeMapper(IMapper mapper) : BaseMapper<App.BLL.DTO.ProductType, v1.ProductType>(mapper);

public class ProductTypeRequestMapper(IMapper mapper) : BaseMapper<App.BLL.DTO.ProductType, v1.ProductTypeRequest>(mapper);