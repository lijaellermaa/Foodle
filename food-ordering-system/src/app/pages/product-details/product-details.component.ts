import {Component, Input, OnInit} from '@angular/core';
import {ProductService} from "../../service/product.service";
import {ToastrService} from "ngx-toastr";
import {CurrencyPipe, NgClass, NgIf, NgOptimizedImage} from "@angular/common";
import {MatIcon} from "@angular/material/icon";
import {CartService} from "../../service/cart.service";
import {Product} from "../../shared/models/Product";

@Component({
  selector: 'app-product-details',
  standalone: true,
  templateUrl: './product-details.component.html',
  imports: [
    NgIf,
    NgClass,
    CurrencyPipe,
    NgOptimizedImage,
    MatIcon
  ],
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent implements OnInit {
  @Input() id!: string;

  product: Product | undefined = undefined;

  quantity: number = 0;
  totalPrice: number = 0;

  constructor(
    private cartService: CartService,
    private productService: ProductService,
    private toastrService: ToastrService,
  ) {
  }

  ngOnInit(): void {
    this.productService.getById(this.id).subscribe({
      next: product => {
        this.product = product;
        this.quantity = this.cartService.totalItemQuantityById(this.product.restaurantId, this.product.id);
        this.updateTotalPrice();
      },
      error: error => {
        console.error(error);
        this.toastrService.error(error);
      }
    });
  }

  updateTotalPrice() {
    this.totalPrice = (this.product?.latestPrice?.value ?? 0) * this.quantity;
  }

  addQuantity(quantity: number) {
    this.quantity += quantity;
    this.updateTotalPrice()
  }

  removeQuantity(quantity: number) {
    if (quantity > 0) {
      this.quantity -= quantity;
      this.updateTotalPrice()
    }
  }

  resetQuantity() {
    this.quantity = 0;
    this.updateTotalPrice()
  }

  addItemToCart() {
    if (this.product === undefined || this.product?.latestPrice === undefined) {
      this.toastrService.error("Product cannot be added");
      return;
    }
    this.cartService.addProductToCart(this.product, this.quantity);
    this.resetQuantity();
  }
}
