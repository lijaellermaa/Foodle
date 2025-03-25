using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class RestaurantRepository(AppDbContext dataContext, IMapper<App.Domain.Restaurant, Restaurant> mapper)
    : BaseEntityRepository<App.Domain.Restaurant, Restaurant, AppDbContext>(dataContext, mapper), IRestaurantRepository
{
    protected override IQueryable<Domain.Restaurant> LoadProperties(IQueryable<Domain.Restaurant> query)
    {
        return query
            .Include(x => x.Location)
            .Include(x => x.Orders)
            .Include(x => x.Products)
            .AsQueryable();
    }
}