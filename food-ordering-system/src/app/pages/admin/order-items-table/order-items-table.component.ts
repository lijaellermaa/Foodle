import {Component, OnInit} from '@angular/core';
import {Router, RouterLink} from "@angular/router";
import {OrderItemService} from "../../../service/orderItem.service";
import {OrderItem} from "../../../shared/models/OrderItem";

@Component({
  selector: 'app-restaurants-table',
  standalone: true,
  imports: [
    RouterLink
  ],
  templateUrl: './order-items-table.component.html',
  styleUrl: './order-items-table.component.css'
})
export class OrderItemsTableComponent implements OnInit {
  orderItems: OrderItem[] = [];

  constructor(
    private orderItemService: OrderItemService,
    private router: Router,
  ) {
  }

  ngOnInit(): void {
    this.orderItemService.getAll().subscribe({
      next: value => this.orderItems = value,
      error: err => console.error(err)
    });
  }

  removeById(id: string) {
    this.orderItemService.delete(id).subscribe({
      next: _ => this.router.navigate([this.router.url])
    });
  }
}
