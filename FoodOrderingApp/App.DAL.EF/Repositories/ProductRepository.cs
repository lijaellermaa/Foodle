using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace App.DAL.EF.Repositories;

public class ProductRepository(AppDbContext dataContext, IMapper<App.Domain.Product, Product> mapper)
    : BaseEntityRepository<App.Domain.Product, Product, AppDbContext>(dataContext, mapper), IProductRepository
{
    private readonly IMapper<App.Domain.Product, Product> _mapper = mapper;

    protected override IQueryable<Domain.Product> LoadProperties(IQueryable<Domain.Product> query)
    {
        return query
            .Include(p => p.Prices)
            .Include(p => p.ProductType)
            .Include(p => p.Restaurant)
            .AsQueryable();
    }

    public override async Task<Product?> RemoveAsync(Guid id, Guid? userId = default)
    {
        var product = await FirstOrDefaultAsync(id);
        return product != null ? await base.RemoveAsync(id, userId) : null;
    }

    public override async Task<Product?> FirstOrDefaultAsync(Guid id, Guid? userId = default, Boolean noTracking = false)
    {
        var query = CreateQuery();
        query = LoadProperties(query);

        var res = await query
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();

        return _mapper.Map(res);
    }

    public override async Task<IEnumerable<Product>> GetAllAsync(Guid? userId = default, bool noTracking = false)
    {
        var query = CreateQuery(userId, noTracking);
        query = LoadProperties(query);

        return await query
            .Select(p => _mapper.Map(p)!)
            .ToListAsync();
    }
}