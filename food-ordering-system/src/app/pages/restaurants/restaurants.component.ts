import {Component, OnInit} from '@angular/core';
import {Restaurant} from "../../shared/models/Restaurant";
import {RouterLink} from "@angular/router";
import {RestaurantService} from "../../service/restaurant.service";
import {ToastrService} from "ngx-toastr";
import {NgForOf, NgIf, NgOptimizedImage} from "@angular/common";

@Component({
  selector: 'app-restaurants',
  standalone: true,
  imports: [
    RouterLink,
    NgForOf,
    NgIf,
    NgOptimizedImage
  ],
  templateUrl: './restaurants.component.html',
  styleUrl: './restaurants.component.css'
})
export class RestaurantsComponent implements OnInit {
  restaurants: Restaurant[] = [];
  dataLoaded = false;

  constructor(private restaurantService: RestaurantService, private toastrService: ToastrService) {
  }

  private loadRestaurants() {
    this.restaurantService.getAll()
      .subscribe({
        next: restaurants => {
          this.restaurants = restaurants;
          this.dataLoaded = true;
          return;
        },
        error: err => {
          console.error(err);
          this.toastrService.error("Failed to load restaurants. Retrying...");
        }
      })
  }

  ngOnInit(): void {
    setInterval(() => {
      if (this.dataLoaded) {
        return;
      }
      this.loadRestaurants()
    }, 3000);
  }
}
