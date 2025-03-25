import {Product} from "./Product";

export type PricePure = {
  id: string;
  value: number;
  previousValue: number;
  productId: string;
  comment: string;
}

export type Price = PricePure & {
  product?: Product;
}


export type PriceRequest = {
  id?: string;
  value: number;
  previousValue: number;
  productId: string;
  comment: string;
}
