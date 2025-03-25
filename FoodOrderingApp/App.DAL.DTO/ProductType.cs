using System.ComponentModel.DataAnnotations;
using Base.Contracts.Domain;
using Base.Domain;

namespace App.DAL.DTO;

public class ProductType : DomainEntity, IDomainEntityId
{
    [MaxLength(45)]
    [MinLength(1)]
    public string Name { get; set; } = default!;

    public ICollection<Product>? Products { get; set; }
}