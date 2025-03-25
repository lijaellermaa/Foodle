﻿using App.Domain.Identity;
using Base.Contracts.Domain;
using Base.Domain;
using Helpers.Base.Entities;

namespace App.Domain;

public class Order : DomainEntity, IDomainEntityId, IDomainEntityAppUser
{
    public OrderStatus Status { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    
    public DeliveryType DeliveryType { get; set; }
    
    public string? DeliverTo { get; set; }
    
    public Guid RestaurantId { get; set; }
    public Restaurant? Restaurant { get; set; }
    
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    public ICollection<OrderItem>? OrderItems { get; set; }
}