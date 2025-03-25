using System.ComponentModel.DataAnnotations;
using Base.Contracts.Domain;

namespace Public.DTO.v1;

public class ProductPure : IDomainEntityId
{
    public Guid Id { get; set; }

    [MaxLength(70)]
    [MinLength(1)]
    public string Name { get; set; } = default!;

    [MaxLength(45)]
    [MinLength(1)]
    public string Size { get; set; } = default!;

    [MaxLength(256)]
    public string? Description { get; set; }

    [MaxLength(512)]
    public string ImageUrl { get; set; } = default!;

    public Guid? LatestPriceId { get; set; }
    public PricePure? LatestPrice { get; set; }

    public Guid ProductTypeId { get; set; }
    public ProductType? ProductType { get; set; }

    public Guid RestaurantId { get; set; }
}

public class Product : ProductPure
{
    public RestaurantPure? Restaurant { get; set; }
}

public class ProductRequest
{
    public Guid? Id { get; set; }

    public Guid ProductTypeId { get; set; }
    public Guid RestaurantId { get; set; }

    [MaxLength(70)]
    [MinLength(1)]
    public string Name { get; set; } = default!;

    [MaxLength(45)]
    public string Size { get; set; } = default!;

    [MaxLength(256)]
    public string? Description { get; set; }

    [MaxLength(512)]
    public string ImageUrl { get; set; } = default!;

}