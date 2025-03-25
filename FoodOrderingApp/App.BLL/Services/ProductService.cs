using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using Base.BLL;
using Base.Contracts;
using Helpers.Base.Extensions;
using Helpers.Base.Filter;

namespace App.BLL.Services;

public class ProductService(IAppUnitOfWork uow, IMapper<Product, DAL.DTO.Product> mapper)
    : BaseEntityService<Product, DAL.DTO.Product, IProductRepository>(uow.ProductRepository, mapper), IProductService
{
    public async Task<Product?> AddWithPrice(Product product, decimal price, decimal? previousPrice = null, string? priceComment = null)
    {
        var mappedProduct = Mapper.Map(product);
        if (mappedProduct == null) return null;
        var createdProduct = uow.ProductRepository.Add(mappedProduct);

        uow.PriceRepository.Add(new App.DAL.DTO.Price
        {
            Value = price,
            PreviousValue = previousPrice,
            ProductId = createdProduct.Id,
            Comment = priceComment,
        });

        uow.ProductRepository.Update(createdProduct);
        await uow.SaveChangesAsync();

        return Mapper.Map(createdProduct);
    }

    public async Task<IEnumerable<Product>> GetAllForRestaurantAsync(
        Guid restaurantId,
        IFilter<Product> filter
    )
    {
        var entities = await uow.ProductRepository
            .GetAllAsync();

        return entities
            .Where(p => p.RestaurantId == restaurantId)
            .Select(x => Mapper.Map(x)!)
            .Filter(filter);
    }
}