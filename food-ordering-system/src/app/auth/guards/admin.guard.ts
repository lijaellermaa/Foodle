import {CanActivateFn} from '@angular/router';
import {inject} from '@angular/core';
import {UserService} from '../../service/user.service';

export const adminGuard: CanActivateFn = (route, state) => {
  const userService = inject(UserService);
  return userService.isAuthenticated && userService.isAdmin;
};
