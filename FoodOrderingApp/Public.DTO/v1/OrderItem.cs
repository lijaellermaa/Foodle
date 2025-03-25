using Base.Contracts.Domain;

namespace Public.DTO.v1;

public class OrderItem : IDomainEntityId
{
    public Guid Id { get; set; }

    public uint Quantity { get; set; }

    public Guid ProductId { get; set; }
    public ProductPure? Product { get; set; }

    public Guid PriceId { get; set; }
    public Price? Price { get; set; }

    public decimal PriceValue { get; set; }

    public Guid OrderId { get; set; }
}

public class OrderItemRequest
{
    public Guid? Id { get; set; }

    public uint Quantity { get; set; }
    public Guid ProductId { get; set; }
    public Guid PriceId { get; set; }
    public Guid? OrderId { get; set; }
}