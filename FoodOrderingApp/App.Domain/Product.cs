using System.ComponentModel.DataAnnotations;
using Base.Contracts.Domain;
using Base.Domain;

namespace App.Domain;

public class Product : DomainEntity, IDomainEntityId
{
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


    public Guid ProductTypeId { get; set; }
    public ProductType? ProductType { get; set; }

    public Guid RestaurantId { get; set; }
    public Restaurant? Restaurant { get; set; }

    public ICollection<Price>? Prices { get; set; }
    public ICollection<OrderItem>? OrderItems { get; set; }
}