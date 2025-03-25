import {Component, OnInit} from '@angular/core';
import {Router, RouterLink} from "@angular/router";
import {Order} from "../../../shared/models/Order";
import {OrderService} from "../../../service/order.service";
import {getDeliveryTypeLabel, getOrderStatusLabel, getPaymentMethodLabel} from "../../../shared/models/enums";

@Component({
  selector: 'app-restaurants-table',
  standalone: true,
  imports: [
    RouterLink
  ],
  templateUrl: './orders-table.component.html',
  styleUrl: './orders-table.component.css'
})
export class OrdersTableComponent implements OnInit {
  orders: Order[] = [];

  constructor(
    private orderService: OrderService,
    private router: Router,
  ) {
  }

  ngOnInit(): void {
    this.orderService.getAll().subscribe({
      next: value => this.orders = value,
      error: err => console.error(err)
    });
  }

  removeById(id: string) {
    this.orderService.delete(id).subscribe({
      next: _ => this.router.navigate([this.router.url])
    });
  }

  protected readonly getPaymentMethodLabel = getPaymentMethodLabel;
  protected readonly getDeliveryTypeLabel = getDeliveryTypeLabel;
  protected readonly getOrderStatusLabel = getOrderStatusLabel;
}
