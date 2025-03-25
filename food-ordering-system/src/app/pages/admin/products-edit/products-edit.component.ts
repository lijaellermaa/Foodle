import {Component, Input, OnInit} from '@angular/core';
import {KeyValuePipe, Location} from '@angular/common';
import {FormBuilder, ReactiveFormsModule, Validators} from "@angular/forms";
import {Restaurant} from "../../../shared/models/Restaurant";
import {RestaurantService} from "../../../service/restaurant.service";
import {ProductType} from "../../../shared/models/ProductType";
import {ProductTypeService} from "../../../service/productType.service";
import {ProductService} from "../../../service/product.service";
import {ProductRequest} from "../../../shared/models/Product";

@Component({
  selector: 'app-orders-edit',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    KeyValuePipe
  ],
  templateUrl: './products-edit.component.html',
  styleUrl: './products-edit.component.css'
})
export class ProductsEditComponent implements OnInit {
  @Input() id?: string;
  productTypes: ProductType[] = [];
  restaurants: Restaurant[] = [];

  formGroup = this.formBuilder.group({
    productTypeId: ["", Validators.required],
    restaurantId: ["", Validators.required],
    name: ["", Validators.required],
    size: ["", Validators.required],
    description: ["", Validators.required],
    imageUrl: ["", Validators.required],
  });

  constructor(
    private productsService: ProductService,
    private restaurantService: RestaurantService,
    private productTypeService: ProductTypeService,
    private location: Location,
    private formBuilder: FormBuilder
  ) {
  }

  ngOnInit(): void {
    this.restaurantService.getAll().subscribe({
      next: value => this.restaurants = value,
      error: err => console.error(err)
    });
    this.productTypeService.getAll().subscribe({
      next: value => this.productTypes = value,
      error: err => console.error(err),
    })
    if (this.id) this.loadData(this.id);
  }

  private loadData(id: string): void {
    this.productsService.getById(id).subscribe({
      next: value => this.formGroup.patchValue(value),
      error: err => console.error("Failed to load data", err)
    });
  }

  postOrUpdate() {
    const request: ProductRequest = {
      id: this.id,
      productTypeId: this.formGroup.value.productTypeId!,
      restaurantId: this.formGroup.value.restaurantId!,
      name: this.formGroup.value.name!,
      size: this.formGroup.value.size!,
      description: this.formGroup.value.description!,
      imageUrl: this.formGroup.value.imageUrl!,
    };
    const result = this.id
      ? this.productsService.update(this.id, request)
      : this.productsService.create(request);

    return result.subscribe({
      next: _ => this.location.back(),
      error: err => console.error("Updating entity failed", err)
    })
  }
}
