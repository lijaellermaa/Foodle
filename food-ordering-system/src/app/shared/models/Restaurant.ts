import {OrderPure} from "./Order";
import {ProductPure} from "./Product";

export type RestaurantPure = {
  id: string;
  name: string;
  phoneNumber: string;
  openTime: string;
  closeTime: string;
  locationId: string;
  imageUrl: string;
}

export type Restaurant = RestaurantPure & {
  location?: Location;
  orders?: OrderPure[];
  products?: ProductPure[];
}

export type RestaurantRequest = {
  id?: string;
  name: string;
  phoneNumber: string;
  openTime: string;
  closeTime: string;
  locationId: string;
  imageUrl: string;
}
