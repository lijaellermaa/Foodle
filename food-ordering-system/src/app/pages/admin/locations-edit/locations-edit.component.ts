import {Component, Input, OnInit} from '@angular/core';
import {Location as NgLocation} from '@angular/common';
import {LocationService} from "../../../service/location.service";
import {LocationRequest} from "../../../shared/models/Location";
import {FormBuilder, ReactiveFormsModule, Validators} from "@angular/forms";

@Component({
  selector: 'app-locations-edit',
  standalone: true,
  imports: [
    ReactiveFormsModule
  ],
  templateUrl: './locations-edit.component.html',
  styleUrl: './locations-edit.component.css'
})
export class LocationsEditComponent implements OnInit {
  @Input() id?: string;

  formGroup = this.formBuilder.group({
    area: ["", Validators.required],
    town: ["", Validators.required],
    address: ["", Validators.required],
  });

  constructor(
    private locationService: LocationService,
    private location: NgLocation,
    private formBuilder: FormBuilder
  ) {
  }

  ngOnInit(): void {
    if (this.id) this.loadData(this.id);
  }

  private loadData(id: string): void {
    this.locationService.getById(id).subscribe({
      next: value => this.formGroup.patchValue(value),
      error: err => console.error("Failed to load data", err)
    });
  }

  postOrUpdate() {
    const request: LocationRequest = {
      id: this.id,
      area: this.formGroup.value.area!,
      town: this.formGroup.value.town!,
      address: this.formGroup.value.address!,
    };
    const result = this.id
      ? this.locationService.update(this.id, request)
      : this.locationService.create(request);

    return result.subscribe({
      next: _ => this.location.back(),
      error: err => console.error("Updating entity failed", err)
    })
  }
}
