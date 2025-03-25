import {Component, Input, OnInit} from '@angular/core';
import {Location} from '@angular/common';
import {FormBuilder, ReactiveFormsModule, Validators} from "@angular/forms";
import {PriceService} from "../../../service/price.service";
import {PriceRequest} from "../../../shared/models/Price";
import {ProductService} from "../../../service/product.service";
import {Product} from "../../../shared/models/Product";

@Component({
  selector: 'app-prices-edit',
  standalone: true,
  imports: [
    ReactiveFormsModule
  ],
  templateUrl: './prices-edit.component.html',
  styleUrl: './prices-edit.component.css'
})
export class PricesEditComponent implements OnInit {
  @Input() id?: string;
  products: Product[] = [];

  formGroup = this.formBuilder.group({
    value: [0, Validators.required],
    previousValue: [0, Validators.required],
    productId: ["", Validators.required],
    comment: ["", Validators.required]
  });

  constructor(
    private priceService: PriceService,
    private productService: ProductService,
    private location: Location,
    private formBuilder: FormBuilder
  ) {
  }

  ngOnInit(): void {
    this.productService.getAll().subscribe({
      next: value => this.products = value,
      error: err => console.error(err)
    });
    if (this.id) this.loadData(this.id);
  }

  private loadData(id: string): void {
    this.priceService.getById(id).subscribe({
      next: value => this.formGroup.patchValue(value),
      error: err => console.error("Failed to load data", err)
    });
  }

  postOrUpdate() {
    const request: PriceRequest = {
      id: this.id,
      value: this.formGroup.value.value!,
      previousValue: this.formGroup.value.previousValue!,
      productId: this.formGroup.value.productId!,
      comment: this.formGroup.value.comment!
    };
    const result = this.id
      ? this.priceService.update(this.id, request)
      : this.priceService.create(request);

    return result.subscribe({
      next: _ => this.location.back(),
      error: err => console.error("Updating entity failed", err)
    })
  }
}
