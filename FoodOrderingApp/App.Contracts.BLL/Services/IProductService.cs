using App.BLL.DTO;
using Base.Contracts.BLL;
using Helpers.Base.Filter;

namespace App.Contracts.BLL.Services;

public interface IProductService : IEntityService<Product>
{
    Task<Product?> AddWithPrice(Product product, decimal price, decimal? previousPrice = null, string? priceComment = null);
    Task<IEnumerable<Product>> GetAllForRestaurantAsync(
        Guid restaurantId,
        IFilter<Product> filter
    );
}