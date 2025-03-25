import {Component, Input, OnInit} from '@angular/core';
import {ActivatedRoute, RouterLink} from '@angular/router';
import {CurrencyPipe, NgClass, NgForOf, NgIf, NgOptimizedImage} from "@angular/common";
import {Product} from "../../shared/models/Product";
import {ProductService} from "../../service/product.service";

@Component({
  selector: 'app-product-list',
  standalone: true,
  templateUrl: './product-list.component.html',
  imports: [
    RouterLink,
    NgIf,
    NgIf,
    NgIf,
    NgForOf,
    NgClass,
    CurrencyPipe,
    NgOptimizedImage
  ],
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
  @Input()
  id!: string;

  products: Product[] = [];
  dataLoaded = false

  constructor(private productService: ProductService, private activatedRoute: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.productService.getAll(this.id, {
      limit: 100
    })
      .subscribe({
        next: products => {
          this.products = products.map(product => {
            return {...product, price: null}
          });
          this.dataLoaded = true
        },
        error: err => console.error(err)
      });
  }
}
