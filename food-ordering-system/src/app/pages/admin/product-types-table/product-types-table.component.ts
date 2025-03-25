import {Component, OnInit} from '@angular/core';
import {ProductType} from "../../../shared/models/ProductType";
import {ProductTypeService} from "../../../service/productType.service";
import {Router, RouterLink} from "@angular/router";

@Component({
  selector: 'app-productTypes-table',
  standalone: true,
  imports: [
    RouterLink
  ],
  templateUrl: './product-types-table.component.html',
  styleUrl: './product-types-table.component.css'
})
export class ProductTypesTableComponent implements OnInit {
  productTypes: ProductType[] = [];

  constructor(
    private productTypeService: ProductTypeService,
    private router: Router,
  ) {
  }

  ngOnInit(): void {
    this.productTypeService.getAll().subscribe({
      next: value => this.productTypes = value,
      error: err => console.error(err)
    });
  }

  removeById(id: string) {
    this.productTypeService.delete(id).subscribe({
      next: _ => this.router.navigate([this.router.url])
    });
  }
}
