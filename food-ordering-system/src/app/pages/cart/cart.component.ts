import {Component} from '@angular/core';
import {Product} from "../../shared/models/Product";
import {TitleComponent} from "../../component/title/title.component";
import {NotFoundComponent} from "../../component/not-found/not-found.component";
import {CurrencyPipe, KeyValuePipe, NgForOf, NgIf} from "@angular/common";
import {RouterLink} from "@angular/router";
import {Carts, CartService} from "../../service/cart.service";

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css'],
  imports: [
    TitleComponent,
    NotFoundComponent,
    KeyValuePipe,
    RouterLink,
    NgForOf,
    CurrencyPipe,
    NgIf
  ],
  standalone: true
})
export class CartComponent {
  constructor(private cartService: CartService) {
  }

  isEmpty(): boolean {
    return this.cartService.isEmpty();
  }

  getCarts(): Carts {
    return this.cartService.carts;
  }

  removeCartById(restaurantId: string) {
    this.cartService.removeCartById(restaurantId);
  }

  totalPriceById(restaurantId: string): number {
    return this.cartService.totalCartPriceById(restaurantId);
  }

  removeItemById(restaurantId: string, restaurantName: string, productId: string, quantity: number): boolean {
    return this.cartService.removeFromCartById(restaurantId, restaurantName, productId, quantity);
  }

  addItemToCart(product: Product, quantity: number) {
    this.cartService.addProductToCart(product, quantity);
  }
}
