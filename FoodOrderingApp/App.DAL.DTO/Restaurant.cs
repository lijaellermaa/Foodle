using System.ComponentModel.DataAnnotations;
using Base.Contracts.Domain;
using Base.Domain;

namespace App.DAL.DTO;

public class Restaurant : DomainEntity, IDomainEntityId
{
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
    public Location? Location { get; set; }

    public ICollection<Order>? Orders { get; set; }

    public ICollection<Product>? Products { get; set; }
}