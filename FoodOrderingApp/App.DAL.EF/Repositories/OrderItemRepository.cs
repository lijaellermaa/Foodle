using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class OrderItemRepository(AppDbContext dataContext, IMapper<App.Domain.OrderItem, OrderItem> mapper)
    : BaseEntityRepository<App.Domain.OrderItem, OrderItem, AppDbContext>(dataContext, mapper), IOrderItemRepository
{
    protected override IQueryable<Domain.OrderItem> LoadProperties(IQueryable<Domain.OrderItem> query)
    {
        return query
            .Include(x => x.Product)
            .Include(x => x.Price)
            .Include(x => x.Order)
            .AsQueryable();
    }

    public override async Task<OrderItem?> RemoveAsync(Guid id, Guid? userId = default)
    {
        var orderItem = await FirstOrDefaultAsync(id);
        return orderItem != null ? await base.RemoveAsync(id, userId) : null;
    }

    public async Task<List<OrderItem?>?> RemoveByOrderIdAsync(Guid orderId, Guid? userId = default)
    {
        var orderItems = await RepositoryDbSet
            .Where(x => x.OrderId == orderId)
            .ToListAsync();
        
        RepositoryDbSet.RemoveRange(orderItems);
        
        return orderItems
            .Select(Mapper.Map)
            .ToList();
    }

    public override async Task<OrderItem?> FirstOrDefaultAsync(Guid id, Guid? userId = default, bool noTracking = false)
    {
        var query = CreateQuery(userId, noTracking);
        query = LoadProperties(query);

        var res = query
            .Where(o => o.Id == id);

        return Mapper.Map(await query.FirstOrDefaultAsync());
    }
}