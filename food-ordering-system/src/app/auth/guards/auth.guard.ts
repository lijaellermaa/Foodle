import {CanActivateFn, Router} from '@angular/router';
import {inject} from '@angular/core';
import {UserService} from '../../service/user.service';

export const authGuard: CanActivateFn = (route, state) => {
  const userService = inject(UserService);
  if (userService.isAuthenticated) {
    return true;
  }

  const router = inject(Router);
  router.navigate(['/login'], {queryParams: {returnUrl: state.url}});
  return false;
};
