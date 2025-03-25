export enum DeliveryType {
  Delivery = 0,
  Pickup = 1,
}

export function getDeliveryTypeLabel(value: DeliveryType): string {
  switch (value) {
    case DeliveryType.Delivery:
      return "Delivery";
    case DeliveryType.Pickup:
      return "Pickup";
  }
}
