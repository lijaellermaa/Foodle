using System.ComponentModel.DataAnnotations;
using Base.Contracts.Domain;
using Base.Domain;

namespace App.DAL.DTO;

public class Location : DomainEntity, IDomainEntityId
{
    [MaxLength(256)]
    [MinLength(1)]
    public string Area { get; set; } = default!;

    [MaxLength(256)]
    [MinLength(1)]
    public string Town { get; set; } = default!;

    [MaxLength(256)]
    [MinLength(1)]
    public string Address { get; set; } = default!;

    public ICollection<Restaurant>? Restaurants { get; set; }
}