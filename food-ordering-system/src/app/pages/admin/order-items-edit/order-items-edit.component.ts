import {Component, Input, OnInit} from '@angular/core';
import {Location as NgLocation} from '@angular/common';
import {FormBuilder, ReactiveFormsModule, Validators} from "@angular/forms";
import {OrderItemService} from "../../../service/orderItem.service";
import {ProductService} from "../../../service/product.service";
import {PriceService} from "../../../service/price.service";
import {OrderService} from "../../../service/order.service";
import {Product} from "../../../shared/models/Product";
import {Price} from "../../../shared/models/Price";
import {OrderItemRequest} from "../../../shared/models/OrderItem";
import {Order} from "../../../shared/models/Order";

@Component({
  selector: 'app-restaurants-edit',
  standalone: true,
  imports: [
    ReactiveFormsModule
  ],
  templateUrl: './order-items-edit.component.html',
  styleUrl: './order-items-edit.component.css'
})
export class OrderItemsEditComponent implements OnInit {
  @Input() id?: string;
  products: Product[] = [];
  prices: Price[] = [];
  orders: Order[] = [];

  formGroup = this.formBuilder.group({
    quantity: [0, Validators.required],
    productId: ["", Validators.required],
    priceId: ["", Validators.required],
    orderId: ["", Validators.required]
  });

  constructor(
    private orderItemService: OrderItemService,
    private productService: ProductService,
    private priceService: PriceService,
    private orderService: OrderService,
    private location: NgLocation,
    private formBuilder: FormBuilder
  ) {
  }

  ngOnInit(): void {
    this.productService.getAll().subscribe({
      next: value => this.products = value,
      error: err => console.error(err)
    });
    this.priceService.getAll().subscribe({
      next: value => this.prices = value,
      error: err => console.error(err)
    });
    this.orderService.getAll().subscribe({
      next: value => this.orders = value,
      error: err => console.error(err)
    });

    if (this.id) this.loadData(this.id);
  }

  private loadData(id: string): void {
    this.orderItemService.getById(id).subscribe({
      next: value => this.formGroup.patchValue(value),
      error: err => console.error("Failed to load data", err)
    });
  }

  postOrUpdate() {
    const request: OrderItemRequest = {
      id: this.id,
      quantity: this.formGroup.value.quantity!,
      productId: this.formGroup.value.productId!,
      priceId: this.formGroup.value.priceId!,
      orderId: this.formGroup.value.orderId!,
    };
    const result = this.id
      ? this.orderItemService.update(this.id, request)
      : this.orderItemService.create(request);

    return result.subscribe({
      next: _ => this.location.back(),
      error: err => console.error("Updating entity failed", err)
    })
  }
}
