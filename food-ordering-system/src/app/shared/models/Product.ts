import {RestaurantPure} from "./Restaurant";
import {Price} from "./Price";
import {ProductType} from "./ProductType";

export type ProductPure = {
  id: string;
  name: string;
  size: string;
  description: string;
  imageUrl: string;
  latestPriceId?: string;
  latestPrice?: Price;
  productTypeId: string;
  productType: ProductType;
  restaurantId: string;
}

export type Product = ProductPure & {
  restaurant: RestaurantPure;
}

export type ProductRequest = {
  id?: string;
  productTypeId: string;
  restaurantId: string;
  name: string;
  size: string;
  description: string;
  imageUrl: string;
}
