import {Injectable} from '@angular/core';
import {UserService} from './user.service';
import {ToastrService} from 'ngx-toastr';
import {Product} from "../shared/models/Product";
import {OrderItemRequest} from "../shared/models/OrderItem";
import {OrderRequest} from "../shared/models/Order";
import {DeliveryType, OrderStatus, PaymentMethod} from "../shared/models/enums";
import {CART_SESSION_KEYS} from "../shared/constants/session";


export type CartItem = OrderItemRequest & {
  product: Product,
};

export type Cart = {
  name: string;
  items: Map<string, CartItem>
};
export type Carts = Map<string, Cart>;

@Injectable({
  providedIn: 'root',
})
export class CartService {
  private readonly _carts: Carts;

  constructor(
    private userService: UserService,
    private toastrService: ToastrService,
  ) {
    this._carts = this.getCartsFromCookies();
  }

  public get carts(): Carts {
    return this._carts;
  }

  public getOrCreateCartById(restaurantId: string, restaurantName: string): Cart {
    const cart = this._carts.get(restaurantId);
    if (cart) return cart;

    this._carts.set(restaurantId, {
      name: restaurantName,
      items: new Map<string, CartItem>(),
    });
    this.saveCartsToCookies(this._carts);
    return this._carts.get(restaurantId)!;
  }

  public getCartById(restaurantId: string): Cart | undefined {
    return this._carts.get(restaurantId);
  }

  public getCartItemsById(restaurantId: string): CartItem[] {
    const cart = this.getCartById(restaurantId);
    if (!cart) return [];
    return Array.from(cart.items.values());
  }

  public totalCartPriceById(restaurantId: string): number {
    const cart = this.getCartById(restaurantId);
    if (!cart) return 0;

    let totalPrice = 0;
    cart.items.forEach(({product, quantity}: CartItem) => {
      totalPrice += quantity * (product.latestPrice?.value ?? 0);
    });
    return totalPrice;
  }

  public totalItemPriceById(restaurantId: string, productId: string): number {
    const cart = this.getCartById(restaurantId);
    if (!cart) return 0;

    const cartItem = cart.items.get(productId);
    return cartItem ? cartItem.quantity * (cartItem.product.latestPrice?.value ?? 0) : 0;
  }

  public totalCartQuantityById(restaurantId: string, restaurantName: string): number {
    const cart = this.getOrCreateCartById(restaurantId, restaurantName);
    let totalQuantity = 0;
    cart.items.forEach(({quantity}: CartItem) => {
      totalQuantity += quantity;
    });
    return totalQuantity;
  }

  public totalItemQuantityById(restaurantId: string, productId: string): number {
    const cart = this.getCartById(restaurantId);
    if (!cart) return 0;

    const cartItem = cart.items.get(productId);
    return cartItem ? cartItem.quantity : 0;
  }

  public addItemToCart(item: CartItem): void {
    const cart = this.getOrCreateCartById(item.product.restaurantId, item.product.restaurant.name);
    let cartItem = cart.items.get(item.productId);
    if (cartItem) {
      cartItem.quantity += item.quantity;
    } else {
      cartItem = item;
    }

    cart.items = cart.items.set(item.productId, cartItem);
    this._carts.set(item.product.restaurantId, cart);

    this.saveCartsToCookies(this._carts);
    this.toastrService.success("", 'Product added to cart successfully');
  }

  public addProductToCart(product: Product, quantity: number): void {
    if (!product.latestPriceId) {
      this.toastrService.error("Product cannot be added");
      return;
    }

    const item: CartItem = {
      product: product,
      productId: product.id,
      priceId: product.latestPriceId,
      quantity: quantity,
    };
    return this.addItemToCart(item);
  }

  public removeFromCartById(restaurantId: string, restaurantName: string, productId: string, quantity: number): boolean {
    const cart = this.getOrCreateCartById(restaurantId, restaurantName);
    const cartItem = cart.items.get(productId);
    if (!cartItem) {
      return false;
    }

    if (cartItem.quantity <= quantity) {
      return this.removeCartById(restaurantId);
    }

    cartItem.quantity -= quantity;
    cart.items = cart.items.set(productId, cartItem)
    this._carts.set(restaurantId, cart);

    this.saveCartsToCookies(this._carts);
    this.toastrService.success("", "Product removed from cart successfully");
    return true;
  }

  public removeCartById(restaurantId: string): boolean {
    const cart = this.getCartById(restaurantId);
    if (!cart) return true;

    const existed = this._carts.delete(restaurantId)
    this.saveCartsToCookies(this._carts);
    return existed;
  }

  public toOrder(restaurantId: string, deliveryType: DeliveryType, paymentMethod: PaymentMethod, orderStatus: OrderStatus = OrderStatus.Created): OrderRequest | undefined {
    if (!this.userService.isAuthenticated || this.userService.currentUser === undefined) {
      return;
    }

    const cartItems = this.getCartItemsById(restaurantId);
    if (cartItems.length < 1) return undefined;

    let orderItems: OrderItemRequest[] = [];
    cartItems.forEach(({productId, quantity, priceId}) => {
      orderItems.push({productId, quantity, priceId});
    })
    return {
      orderItems,
      deliveryType,
      paymentMethod,
      status: orderStatus,
      restaurantId,
      appUserId: this.userService.currentUser.id,
      deliverTo: this.userService.currentUser.address,
    };
  }

  public size(): number {
    return this._carts.size;
  }

  public isEmpty(): boolean {
    return this._carts.size <= 0;
  }

  private getCartsFromCookies(): Carts {
    const cartsString = localStorage.getItem(CART_SESSION_KEYS.carts);
    if (!cartsString) return new Map<string, Cart>;
    return JSON.parse(cartsString, (key, value) => {
      if (typeof value === 'object' && value !== null) {
        if (value.dataType === 'Map') {
          return new Map(value.value);
        }
      }
      return value;
    }) as Map<string, Cart>;
  }

  private saveCartsToCookies(carts: Carts): void {
    localStorage.setItem(CART_SESSION_KEYS.carts, JSON.stringify(carts, (key, value) => {
      if (value instanceof Map) {
        return {
          dataType: 'Map',
          value: Array.from(value.entries()), // or with spread: value: [...value]
        };
      } else {
        return value;
      }
    }));
  }
}
