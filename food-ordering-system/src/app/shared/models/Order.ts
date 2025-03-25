import {DeliveryType, OrderStatus, PaymentMethod} from "./enums";
import {OrderItem, OrderItemRequest} from "./OrderItem";
import {RestaurantPure} from "./Restaurant";

export type OrderPure = {
  id: string;
  totalPrice: number;

  status: OrderStatus;
  paymentMethod: PaymentMethod;
  deliveryType: DeliveryType;

  deliverTo?: string;

  restaurantId: string;
  restaurant: RestaurantPure;

  appUserId: string;
}

export type Order = OrderPure & {
  orderItems?: OrderItem[];
}

export type OrderRequest = {
  id?: string;

  paymentMethod: PaymentMethod;
  deliveryType: DeliveryType;
  status: OrderStatus;
  deliverTo?: string;
  restaurantId: string;
  appUserId: string;

  orderItems?: OrderItemRequest[];
}
