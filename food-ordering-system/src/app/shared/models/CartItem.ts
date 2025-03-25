import {Product} from "./Product";

export class CartItem {
  id?: string;
  quantity: number = 1;
  productId!: string;
  product?: Product;
  orderId?: string;
}
