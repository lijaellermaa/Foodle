import {Component, Input, OnInit} from '@angular/core';
import {ReactiveFormsModule} from '@angular/forms';
import {ToastrService} from 'ngx-toastr';
import {OrderService} from '../../service/order.service';
import {DatePipe} from '@angular/common';
import {DeliveryType, PaymentMethod} from '../../shared/models/enums';
import {Router} from "@angular/router";
import {TitleComponent} from "../../component/title/title.component";
import {TextInputComponent} from "../../component/text-input/text-input.component";
import {OrderItemsListComponent} from "../../component/order-items-list/order-items-list.component";
import {UserService} from "../../service/user.service";
import {CartItem, CartService} from "../../service/cart.service";

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.css'],
  providers: [DatePipe],
  imports: [
    TitleComponent,
    TextInputComponent,
    OrderItemsListComponent,
    ReactiveFormsModule
  ],
  standalone: true
})
export class CheckoutComponent implements OnInit {
  @Input()
  id!: string;

  cartItems: CartItem[] = [];
  name: string | undefined;
  address: string | undefined;

  constructor(
    public cartService: CartService,
    private toastrService: ToastrService,
    private orderService: OrderService,
    private userService: UserService,
    private router: Router,
  ) {
  }

  ngOnInit(): void {
    this.cartItems = this.cartService.getCartItemsById(this.id);
    this.userService.session$.subscribe({
      next: value => {
        if (!value) return;
        this.name = `${value.user.firstName} ${value.user.lastName}`;
        this.address = value.user.address;
      }
    })
  }

  createOrder() {
    const order = this.cartService.toOrder(this.id, DeliveryType.Delivery, PaymentMethod.CreditCard)
    if (!order) {
      return;
    }

    this.orderService.create(order).subscribe({
      next: _ => {
        this.toastrService.success('Order successfully created.');
        this.cartService.removeCartById(this.id);
        this.router.navigate(['/orders']);
      },
      error: error => {
        this.toastrService.error(error);
      }
    });
  }
}
