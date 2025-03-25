import {Component, Input, OnInit} from '@angular/core';
import {Location as NgLocation} from '@angular/common';
import {ProductTypeService} from "../../../service/productType.service";
import {ProductTypeRequest} from "../../../shared/models/ProductType";
import {FormBuilder, ReactiveFormsModule, Validators} from "@angular/forms";

@Component({
  selector: 'app-productTypes-edit',
  standalone: true,
  imports: [
    ReactiveFormsModule
  ],
  templateUrl: './product-types-edit.component.html',
  styleUrl: './product-types-edit.component.css'
})
export class ProductTypesEditComponent implements OnInit {
  @Input() id?: string;

  formGroup = this.formBuilder.group({
    name: ["", Validators.required],
  });

  constructor(
    private productTypeService: ProductTypeService,
    private location: NgLocation,
    private formBuilder: FormBuilder
  ) {
  }

  ngOnInit(): void {
    if (this.id) this.loadData(this.id);
  }

  private loadData(id: string): void {
    this.productTypeService.getById(id).subscribe({
      next: value => this.formGroup.patchValue(value),
      error: err => console.error("Failed to load data", err)
    });
  }

  postOrUpdate() {
    const request: ProductTypeRequest = {
      id: this.id,
      name: this.formGroup.value.name!,
    };
    const result = this.id
      ? this.productTypeService.update(this.id, request)
      : this.productTypeService.create(request);

    return result.subscribe({
      next: _ => this.location.back(),
      error: err => console.error("Updating entity failed", err)
    })
  }
}
