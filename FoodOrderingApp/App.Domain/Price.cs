using Base.Contracts.Domain;
using Base.Domain;

namespace App.Domain;

public class Price : DomainEntity, IDomainEntityId
{
    public decimal Value { get; set; } = default!;
    public decimal? PreviousValue { get; set; }

    public Guid ProductId { get; set; } = default!;
    public Product? Product { get; set; }
    
    public string? Comment { get; set; }
}