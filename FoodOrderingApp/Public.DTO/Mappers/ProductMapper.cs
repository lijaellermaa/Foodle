using AutoMapper;
using Base.DAL;

namespace Public.DTO.Mappers;

public class ProductMapper(IMapper mapper) : BaseMapper<App.BLL.DTO.Product, v1.Product>(mapper);

public class ProductRequestMapper(IMapper mapper) : BaseMapper<App.BLL.DTO.Product, v1.ProductRequest>(mapper);