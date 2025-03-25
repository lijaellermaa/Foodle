import {Component, Input} from '@angular/core';
import {CartItem} from "../../service/cart.service";
import {CurrencyPipe, NgForOf} from "@angular/common";
import {RouterLink} from "@angular/router";

@Component({
  selector: 'order-items-list',
  standalone: true,
  templateUrl: './order-items-list.component.html',
  imports: [
    NgForOf,
    RouterLink,
    CurrencyPipe,
  ],
  styleUrls: ['./order-items-list.component.css']
})
export class OrderItemsListComponent {
  @Input()
  items!: CartItem[];

  constructor() {
  }

  getTotalPrice(): number {
    let totalPrice = 0;
    for (const item of this.items) {
      totalPrice += item.quantity * (item.product.latestPrice?.value ?? 0);
    }
    return totalPrice;
  }
}
