export enum OrderStatus {
  Created = 0,
  InDelivery = 1,
  Archived = 2,
  Completed = 3,
}

export function getOrderStatusLabel(value: OrderStatus): string {
  switch (value) {
    case OrderStatus.InDelivery:
      return "In Delivery";
    case OrderStatus.Created:
      return "Created";
    case OrderStatus.Archived:
      return "Archived"
    case OrderStatus.Completed:
      return "Completed"
  }
}
