using Base.Contracts.Domain;
using Helpers.Base.Entities;

namespace Public.DTO.v1;

public class OrderPure : IDomainEntityId
{
    public Guid Id { get; set; }
    
    public decimal TotalPrice { get; set; }

    public OrderStatus Status { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public DeliveryType DeliveryType { get; set; }

    public string? DeliverTo { get; set; }

    public Guid RestaurantId { get; set; }
    public Guid AppUserId { get; set; }
}

public class Order : OrderPure 
{
    public ICollection<OrderItem>? OrderItems { get; set; }
}

public class OrderRequest
{
    public Guid? Id { get; set; }

    public PaymentMethod PaymentMethod { get; set; }
    public DeliveryType DeliveryType { get; set; }
    public OrderStatus Status { get; set; }
    public string? DeliverTo { get; set; }
    public Guid RestaurantId { get; set; }
    public Guid AppUserId { get; set; }
}

public class OrderWithItemsRequest : OrderRequest
{
    public ICollection<OrderItemRequest> OrderItems { get; set; } = new List<OrderItemRequest>();
}