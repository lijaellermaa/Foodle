import {ApplicationConfig, importProvidersFrom} from '@angular/core';
import {provideRouter, withComponentInputBinding, withRouterConfig} from '@angular/router';

import routes from './app.routes';
import {provideHttpClient, withInterceptors} from "@angular/common/http";
import {provideToastr, ToastNoAnimation} from "ngx-toastr";
import {CookieService} from "ngx-cookie-service";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import {MatSelectModule} from "@angular/material/select";
import {NgOptimizedImage} from "@angular/common";
import {tokenInterceptor} from "./auth/token.interceptor";
import {MatIconModule} from "@angular/material/icon";

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes, withComponentInputBinding(), withRouterConfig({
      onSameUrlNavigation: "reload"
    })),
    provideHttpClient(withInterceptors([
      tokenInterceptor,
    ])),
    CookieService,
    importProvidersFrom([
      BrowserAnimationsModule,
      ReactiveFormsModule,
      BrowserAnimationsModule,
      MatFormFieldModule,
      MatIconModule,
      MatInputModule,
      MatSelectModule,
      FormsModule,
      NgOptimizedImage
    ]),
    provideToastr({
      toastComponent: ToastNoAnimation,
      positionClass: "toast-bottom-right"
    }),
  ]
};
