using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class ProductTypeRepository : BaseEntityRepository<App.Domain.ProductType, ProductType, AppDbContext>, IProductTypeRepository
{
    public ProductTypeRepository(AppDbContext dataContext, IMapper<App.Domain.ProductType, ProductType> mapper) : base(dataContext, mapper)
    {
    }
}