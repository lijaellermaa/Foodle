import {Component, OnInit} from '@angular/core';
import {UserService} from "../../service/user.service";
import {UserPayload} from "../../shared/models/Auth";
import {CartService} from "../../service/cart.service";
import {RouterLink} from "@angular/router";
import {NgIf} from "@angular/common";

@Component({
  selector: 'app-header',
  standalone: true,
  templateUrl: './header.component.html',
  imports: [
    RouterLink,
    NgIf
  ],
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  user: UserPayload | undefined;

  constructor(private userService: UserService, private cartService: CartService) {
  }

  ngOnInit(): void {
    this.userService.session$.subscribe({
      next: (value) => {
        if (value != undefined) {
          this.user = value.user;
        }
      },
    })
  }

  isAuthenticated() {
    return this.userService.isAuthenticated;
  }

  isAdmin(): boolean {
    return this.userService.isAuthenticated && this.userService.isAdmin;
  }

  cartIsEmpty(): boolean {
    return this.cartService.isEmpty();
  }

  cartsSize(): number {
    return this.cartService.size()
  }

  logout() {
    this.userService.logout();
  }
}
