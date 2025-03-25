import {Product} from "./Product";
import {Price} from "./Price";

export type OrderItem = {
  id: string;

  quantity: number;
  productId: string;
  product: Product;

  priceId: string;
  price: Price;

  priceValue: string;
  orderId: string;
}

export type OrderItemRequest = {
  id?: string;
  quantity: number;
  productId: string;
  priceId: string;
  orderId?: string;
}
