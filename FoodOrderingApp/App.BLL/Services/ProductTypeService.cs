using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class ProductTypeService(IAppUnitOfWork uow, IMapper<DTO.ProductType, App.DAL.DTO.ProductType> mapper)
    : BaseEntityService<DTO.ProductType, App.DAL.DTO.ProductType, IProductTypeRepository>(uow.ProductTypeRepository, mapper), IProductTypeService
{
}