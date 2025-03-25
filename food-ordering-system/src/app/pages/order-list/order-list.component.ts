import {Component, OnInit} from '@angular/core';
import {ToastrService} from "ngx-toastr";
import {CurrencyPipe, NgForOf} from "@angular/common";
import {DeliveryType, OrderStatus, PaymentMethod} from "../../shared/models/enums";
import {Order} from "../../shared/models/Order";
import {UserService} from "../../service/user.service";
import {OrderService} from "../../service/order.service";

@Component({
  selector: 'app-order-list',
  standalone: true,
  templateUrl: './order-list.component.html',
  imports: [
    NgForOf,
    CurrencyPipe
  ],
  styleUrls: ['./order-list.component.css']
})
export class OrderListComponent implements OnInit {
  public orderList: Order[];

  constructor(private orderService: OrderService, private userService: UserService, private toastrService: ToastrService) {
    this.orderList = [];
  }

  ngOnInit() {
    if (!this.userService.isAuthenticated) {
      return;
    }
    this.orderService
      .getAll()
      .subscribe({
        next: orders => {
          this.orderList = orders;
        },
        error: err => {
          this.toastrService.error(err);
        }
      })
  }

  protected readonly OrderService = OrderService;
  protected readonly OrderStatus = OrderStatus;
  protected readonly PaymentMethod = PaymentMethod;
  protected readonly DeliveryType = DeliveryType;
}
