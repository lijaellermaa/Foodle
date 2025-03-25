using Base.Contracts.Domain;
using Base.Domain;

namespace App.Domain;

public class OrderItem : DomainEntity, IDomainEntityId
{
    public uint Quantity { get; set; }

    public Guid ProductId { get; set; }
    public Product? Product { get; set; }
    
    public Guid PriceId { get; set; }
    public Price? Price { get; set; }

    public Guid OrderId { get; set; }
    public Order? Order { get; set; }
}