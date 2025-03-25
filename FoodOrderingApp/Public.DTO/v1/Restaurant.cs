using System.ComponentModel.DataAnnotations;
using Base.Contracts.Domain;

namespace Public.DTO.v1;

public class RestaurantPure : IDomainEntityId
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string OpenTime { get; set; } = default!;
    public string CloseTime { get; set; } = default!;
    
    [MaxLength(512)]
    public string ImageUrl { get; set; } = default!;

    public Guid LocationId { get; set; }
}

public class Restaurant : RestaurantPure
{
    public Location? Location { get; set; }
    
    public ICollection<OrderPure>? Orders { get; set; }
    public ICollection<ProductPure>? Products { get; set; }
}

public class RestaurantRequest
{
    public Guid? Id { get; set; }

    [MaxLength(45)]
    [MinLength(1)]
    public string Name { get; set; } = default!;

    [MaxLength(45)]
    [MinLength(1)]
    public string PhoneNumber { get; set; } = default!;

    [MaxLength(45)]
    [MinLength(1)]
    public string OpenTime { get; set; } = default!;

    [MaxLength(45)]
    [MinLength(1)]
    public string CloseTime { get; set; } = default!;
    
    [MaxLength(512)]
    public string ImageUrl { get; set; } = default!;

    public Guid LocationId { get; set; }
}