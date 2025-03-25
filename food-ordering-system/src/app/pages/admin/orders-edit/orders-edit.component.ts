import {Component, Input, OnInit} from '@angular/core';
import {KeyValuePipe, Location} from '@angular/common';
import {OrderService} from "../../../service/order.service";
import {OrderRequest} from "../../../shared/models/Order";
import {FormBuilder, ReactiveFormsModule, Validators} from "@angular/forms";
import {
  DeliveryType,
  getDeliveryTypeLabel,
  getOrderStatusLabel,
  getPaymentMethodLabel,
  OrderStatus,
  PaymentMethod
} from "../../../shared/models/enums";
import {Restaurant} from "../../../shared/models/Restaurant";
import {RestaurantService} from "../../../service/restaurant.service";

@Component({
  selector: 'app-orders-edit',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    KeyValuePipe
  ],
  templateUrl: './orders-edit.component.html',
  styleUrl: './orders-edit.component.css'
})
export class OrdersEditComponent implements OnInit {
  @Input() id?: string;
  restaurants: Restaurant[] = [];

  protected readonly getDeliveryTypeLabel = getDeliveryTypeLabel;
  protected readonly getPaymentMethodLabel = getPaymentMethodLabel;
  protected readonly getOrderStatusLabel = getOrderStatusLabel;
  protected readonly DeliveryType = DeliveryType;
  protected readonly PaymentMethod = PaymentMethod;
  protected readonly OrderStatus = OrderStatus;

  formGroup = this.formBuilder.group({
    paymentMethod: [PaymentMethod.CreditCard, Validators.required],
    deliveryType: [DeliveryType.Delivery, Validators.required],
    orderStatus: [OrderStatus.Created, Validators.required],
    deliverTo: ["", Validators.required],
    restaurantId: ["", Validators.required],
    appUserId: ["", Validators.required],
  });

  constructor(
    private orderService: OrderService,
    private restaurantService: RestaurantService,
    private location: Location,
    private formBuilder: FormBuilder
  ) {
  }

  ngOnInit(): void {
    this.restaurantService.getAll().subscribe({
      next: value => this.restaurants = value,
      error: err => console.error(err)
    });
    if (this.id) this.loadData(this.id);
  }

  private loadData(id: string): void {
    this.orderService.getById(id).subscribe({
      next: value => this.formGroup.patchValue(value),
      error: err => console.error("Failed to load data", err)
    });
  }

  postOrUpdate() {
    const request: OrderRequest = {
      id: this.id,
      paymentMethod: this.formGroup.value.paymentMethod!,
      deliveryType: this.formGroup.value.deliveryType!,
      status: this.formGroup.value.orderStatus!,
      deliverTo: this.formGroup.value.deliverTo!,
      restaurantId: this.formGroup.value.restaurantId!,
      appUserId: this.formGroup.value.restaurantId!,
    };
    const result = this.id
      ? this.orderService.update(this.id, request)
      : this.orderService.create(request);

    return result.subscribe({
      next: _ => this.location.back(),
      error: err => console.error("Updating entity failed", err)
    })
  }

}
