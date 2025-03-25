import {Component, Input, OnInit} from '@angular/core';
import {Location as NgLocation} from '@angular/common';
import {RestaurantService} from "../../../service/restaurant.service";
import {RestaurantRequest} from "../../../shared/models/Restaurant";
import {type Location} from "../../../shared/models/Location";
import {FormBuilder, ReactiveFormsModule, Validators} from "@angular/forms";
import {LocationService} from "../../../service/location.service";

@Component({
  selector: 'app-restaurants-edit',
  standalone: true,
  imports: [
    ReactiveFormsModule
  ],
  templateUrl: './restaurants-edit.component.html',
  styleUrl: './restaurants-edit.component.css'
})
export class RestaurantsEditComponent implements OnInit {
  @Input() id?: string;
  locations: Location[] = [];

  formGroup = this.formBuilder.group({
    name: ["", Validators.required],
    phoneNumber: ["", Validators.required],
    openTime: ["", Validators.required],
    closeTime: ["", Validators.required],
    locationId: ["", Validators.required],
    imageUrl: ["", Validators.required],
  });

  constructor(
    private restaurantService: RestaurantService,
    private locationService: LocationService,
    private location: NgLocation,
    private formBuilder: FormBuilder
  ) {
  }

  ngOnInit(): void {
    this.locationService.getAll().subscribe({
      next: value => this.locations = value,
      error: err => console.error(err)
    });
    if (this.id) this.loadData(this.id);
  }

  private loadData(id: string): void {
    this.restaurantService.getById(id).subscribe({
      next: value => this.formGroup.patchValue(value),
      error: err => console.error("Failed to load data", err)
    });
  }

  postOrUpdate() {
    const request: RestaurantRequest = {
      id: this.id,
      name: this.formGroup.value.name!,
      phoneNumber: this.formGroup.value.phoneNumber!,
      openTime: this.formGroup.value.openTime!,
      closeTime: this.formGroup.value.closeTime!,
      imageUrl: this.formGroup.value.imageUrl!,
      locationId: this.formGroup.value.locationId!,
    };
    const result = this.id
      ? this.restaurantService.update(this.id, request)
      : this.restaurantService.create(request);

    return result.subscribe({
      next: _ => this.location.back(),
      error: err => console.error("Updating entity failed", err)
    })
  }
}
