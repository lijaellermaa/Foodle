import {Component, OnInit} from '@angular/core';
import {Location} from "../../../shared/models/Location";
import {LocationService} from "../../../service/location.service";
import {Router, RouterLink} from "@angular/router";

@Component({
  selector: 'app-locations-table',
  standalone: true,
  imports: [
    RouterLink
  ],
  templateUrl: './locations-table.component.html',
  styleUrl: './locations-table.component.css'
})
export class LocationsTableComponent implements OnInit {
  locations: Location[] = [];

  constructor(
    private locationService: LocationService,
    private router: Router,
  ) {
  }

  ngOnInit(): void {
    this.locationService.getAll().subscribe({
      next: value => this.locations = value,
      error: err => console.error(err)
    });
  }

  removeById(id: string) {
    this.locationService.delete(id).subscribe({
      next: _ => this.router.navigate([this.router.url])
    });
  }
}
