using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.EF;
using Helpers.Base.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace App.DAL.EF.Repositories;

public class OrderRepository(AppDbContext dataContext, IMapper<App.Domain.Order, Order> mapper)
    : BaseEntityRepository<App.Domain.Order, Order, AppDbContext>(dataContext, mapper), IOrderRepository
{

    protected override IQueryable<Domain.Order> LoadProperties(IQueryable<Domain.Order> query)
    {
        return query
            .Include(x => x.OrderItems)
            .Include(x => x.Restaurant)
            .Include(x => x.AppUser)
            .AsQueryable();
    }
    public override async Task<Order?> RemoveAsync(Guid id, Guid? userId = default)
    {
        var data = await base.FirstOrDefaultAsync(id, userId, true);
        return data != null && data.AppUserId == userId ? await base.RemoveAsync(id, userId) : null;
    }
    
    public async Task<bool> IsOwnedByUserAsync(Guid id, Guid userId)
    {
        return await RepositoryDbSet.AnyAsync(t => t.Id == id && t.AppUserId == userId);
    }

    public async Task<Order?> FindByUserAndStatusAsync(Guid userId, OrderStatus status, bool noTracking = false)
    {
        var query = CreateQuery(userId, noTracking);
        query = LoadProperties(query);

        return await query
            .Where(o => o.AppUserId == userId && o.Status == status)
            .Select(o => PopulateAndMapEntity(o, mapper))
            .FirstOrDefaultAsync();
    }

    private static Order PopulateAndMapEntity(App.Domain.Order? entity, IMapper<App.Domain.Order, Order> mapper)
    {
        var mapped = mapper.Map(entity)!;
        if (mapped is { OrderItems: null } || mapped.OrderItems.IsNullOrEmpty()) return mapped;

        var totalPrice = mapped.OrderItems.Sum(p => (p.Price?.Value ?? 0) * p.Quantity);
        mapped.TotalPrice = totalPrice;
        return mapped;
    }

    public override async Task<IEnumerable<Order>> GetAllAsync(Guid? userId = default, Boolean noTracking = false)
    {
        var query = CreateQuery(userId, noTracking);
        query = LoadProperties(query);

        return await query
            .Select(o => PopulateAndMapEntity(o, mapper))
            .ToListAsync();
    }

    public override async Task<Order?> FirstOrDefaultAsync(Guid id, Guid? userId = default, bool noTracking = false)
    {
        var query = CreateQuery(userId, noTracking);
        query = LoadProperties(query);

        var data = await query
            .Where(o => o.Id == id)
            .Select(o => PopulateAndMapEntity(o, mapper))
            .FirstOrDefaultAsync();

        return data?.AppUserId != userId ? null : data;
    }
}