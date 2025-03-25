import {Component, OnInit} from '@angular/core';
import {Restaurant} from "../../../shared/models/Restaurant";
import {RestaurantService} from "../../../service/restaurant.service";
import {Router, RouterLink} from "@angular/router";

@Component({
  selector: 'app-restaurants-table',
  standalone: true,
  imports: [
    RouterLink
  ],
  templateUrl: './restaurants-table.component.html',
  styleUrl: './restaurants-table.component.css'
})
export class RestaurantsTableComponent implements OnInit {
  restaurants: Restaurant[] = [];

  constructor(
    private restaurantService: RestaurantService,
    private router: Router,
  ) {
  }

  ngOnInit(): void {
    this.restaurantService.getAll().subscribe({
      next: value => this.restaurants = value,
      error: err => console.error(err)
    });
  }

  removeById(id: string) {
    this.restaurantService.delete(id).subscribe({
      next: _ => this.router.navigate([this.router.url])
    });
  }
}
