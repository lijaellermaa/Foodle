import {Component, OnInit} from '@angular/core';
import {Router, RouterLink} from "@angular/router";
import {Price} from "../../../shared/models/Price";
import {PriceService} from "../../../service/price.service";

@Component({
  selector: 'app-restaurants-table',
  standalone: true,
  imports: [
    RouterLink
  ],
  templateUrl: './prices-table.component.html',
  styleUrl: './prices-table.component.css'
})
export class PricesTableComponent implements OnInit {
  prices: Price[] = [];

  constructor(
    private priceService: PriceService,
    private router: Router,
  ) {
  }

  ngOnInit(): void {
    this.priceService.getAll().subscribe({
      next: value => this.prices = value,
      error: err => console.error(err)
    });
  }

  removeById(id: string) {
    this.priceService.delete(id).subscribe({
      next: _ => this.router.navigate([this.router.url])
    });
  }
}
