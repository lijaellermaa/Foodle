using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class PriceRepository(AppDbContext dataContext, IMapper<App.Domain.Price, Price> mapper)
    : BaseEntityRepository<App.Domain.Price, Price, AppDbContext>(dataContext, mapper), IPriceRepository
{
    public async Task<ICollection<Price>> GetAllForProductAsync(Guid productId, Guid? userId = default, Boolean noTracking = false)
    {
        var query = CreateQuery(userId, noTracking);
        query = LoadProperties(query);
        return await query
            .Select(p => Mapper.Map(p)!)
            .Where(p => p.ProductId == productId)
            .ToListAsync();
    }

    public async Task<Price?> FirstLatestForProductAsync(Guid productId, Guid? userId = default, bool noTracking = false)
    {
        var query = CreateQuery(userId, noTracking);
        query = LoadProperties(query);
        return await query
            .Select(p => Mapper.Map(p)!)
            .Where(p => p.ProductId == productId)
            .OrderByDescending(p => p.CreatedAt)
            .FirstOrDefaultAsync();
    }


}