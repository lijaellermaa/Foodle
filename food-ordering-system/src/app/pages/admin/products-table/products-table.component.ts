import {Component, OnInit} from '@angular/core';
import {Router, RouterLink} from "@angular/router";
import {ProductService} from "../../../service/product.service";
import {Product} from "../../../shared/models/Product";

@Component({
  selector: 'app-restaurants-table',
  standalone: true,
  imports: [
    RouterLink
  ],
  templateUrl: './products-table.component.html',
  styleUrl: './products-table.component.css'
})
export class ProductsTableComponent implements OnInit {
  products: Product[] = [];

  constructor(
    private productService: ProductService,
    private router: Router,
  ) {
  }

  ngOnInit(): void {
    this.productService.getAll().subscribe({
      next: value => this.products = value,
      error: err => console.error(err)
    });
  }

  removeById(id: string) {
    this.productService.delete(id).subscribe({
      next: _ => this.router.navigate([this.router.url])
    });
  }
}
