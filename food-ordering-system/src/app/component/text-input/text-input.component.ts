import {Component, Input, OnInit} from '@angular/core';
import {AbstractControl, FormControl, ReactiveFormsModule} from '@angular/forms';
import {InputValidationComponent} from "../input-validation/input-validation.component";
import {InputContainerComponent} from "../input-container/input-container.component";

@Component({
  selector: 'text-input',
  standalone: true,
  templateUrl: './text-input.component.html',
  imports: [
    InputValidationComponent,
    InputContainerComponent,
    ReactiveFormsModule
  ],
  styleUrls: ['./text-input.component.css']
})
export class TextInputComponent implements OnInit {
  @Input()
  control!: AbstractControl;
  @Input()
  showErrorsWhen: boolean = true;
  @Input()
  label!: string;
  @Input()
  type: 'text' | 'password' | 'email' = 'text';
  @Input()
  disabled: boolean = false;

  constructor() {
  }

  get formControl() {
    return this.control as FormControl;
  }

  ngOnInit(): void {
  }
}
