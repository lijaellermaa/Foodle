import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {ActivatedRoute, Router} from '@angular/router';
import {UserService} from "../../service/user.service";
import {ChangeProfileRequest, UserPayload} from "../../shared/models/Auth";
import {TextInputComponent} from "../../component/text-input/text-input.component";
import {TitleComponent} from "../../component/title/title.component";

@Component({
  selector: 'app-profile',
  standalone: true,
  templateUrl: './profile.component.html',
  imports: [
    ReactiveFormsModule,
    TextInputComponent,
    TitleComponent
  ],
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  profileForm!: FormGroup;
  isSubmitted = false;
  returnUrl = '';

  private user: UserPayload | undefined;

  constructor(private formBuilder: FormBuilder,
              private activatedRoute: ActivatedRoute,
              private router: Router,
              private userService: UserService) {
  }

  get fc() {
    return this.profileForm.controls;
  }

  ngOnInit(): void {
    this.profileForm = this.formBuilder.group({
      firstname: [this.userService.currentUser?.firstName ?? '', [Validators.required, Validators.minLength(3)]],
      lastname: [this.userService.currentUser?.lastName ?? '', [Validators.required, Validators.minLength(3)]],
      address: [this.userService.currentUser?.address ?? '', [Validators.required, Validators.minLength(5)]]
    });

    this.userService.session$.subscribe({
      next: value => {
        if (!value) return;
        this.user = value.user;
      }
    })

    this.returnUrl = this.activatedRoute.snapshot.queryParams['returnUrl'] ?? '';
  }

  submit() {
    this.isSubmitted = true;
    if (this.profileForm.invalid) return;

    const updatedProfile: ChangeProfileRequest = {
      firstName: this.fc['firstname'].value,
      lastName: this.fc['lastname'].value,
      address: this.fc['address'].value,
      email: this.user?.email,
    }

    this.userService.changeProfile(updatedProfile).subscribe(_ => {
      this.router.navigate([this.returnUrl]).then(r => r);
    });
  }
}
