import {Routes} from '@angular/router';
import {ProfileComponent} from './pages/profile/profile.component';
import {CheckoutComponent} from './pages/checkout/checkout.component';
import {HomeComponent} from './pages/home/home.component';
import {CartComponent} from './pages/cart/cart.component';
import {ProductListComponent} from './pages/product-list/product-list.component';
import {ProductDetailsComponent} from './pages/product-details/product-details.component';
import {LoginComponent} from './pages/login/login.component';
import {RegisterComponent} from './pages/register/register.component';
import {authGuard} from "./auth/guards/auth.guard";
import {OrderListComponent} from './pages/order-list/order-list.component';
import {RestaurantsComponent} from "./pages/restaurants/restaurants.component";
import {adminGuard} from "./auth/guards/admin.guard";
import {AdminComponent} from "./pages/admin/admin.component";
import {RestaurantsTableComponent} from "./pages/admin/restaurants-table/restaurants-table.component";
import {RestaurantsEditComponent} from "./pages/admin/restaurants-edit/restaurants-edit.component";
import {LocationsEditComponent} from "./pages/admin/locations-edit/locations-edit.component";
import {LocationsTableComponent} from "./pages/admin/locations-table/locations-table.component";
import {ProductTypesTableComponent} from "./pages/admin/product-types-table/product-types-table.component";
import {ProductTypesEditComponent} from "./pages/admin/product-types-edit/product-types-edit.component";
import {OrdersTableComponent} from "./pages/admin/orders-table/orders-table.component";
import {OrderItemsTableComponent} from "./pages/admin/order-items-table/order-items-table.component";
import {OrderItemsEditComponent} from "./pages/admin/order-items-edit/order-items-edit.component";
import {PricesTableComponent} from "./pages/admin/prices-table/prices-table.component";
import {PricesEditComponent} from "./pages/admin/prices-edit/prices-edit.component";
import {ProductsTableComponent} from "./pages/admin/products-table/products-table.component";
import {OrdersEditComponent} from "./pages/admin/orders-edit/orders-edit.component";
import {ProductsEditComponent} from "./pages/admin/products-edit/products-edit.component";

export const APP_ROUTES: Routes = [
  {path: '', pathMatch: 'full', component: HomeComponent},
  {path: 'cart', component: CartComponent},
  {path: 'restaurants/:id', component: ProductListComponent},
  {path: 'products/:id', component: ProductDetailsComponent},
  {path: 'restaurants', component: RestaurantsComponent},
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegisterComponent},
  {path: 'profile', component: ProfileComponent, canActivate: [authGuard]},
  {path: 'orders', component: OrderListComponent, canActivate: [authGuard]},
  {path: 'checkout/:id', component: CheckoutComponent, canActivate: [authGuard]},

  {path: 'admin', component: AdminComponent, canActivate: [adminGuard]},

  {path: 'admin/restaurants', component: RestaurantsTableComponent, canActivate: [adminGuard]},
  {path: 'admin/restaurants/edit/:id', component: RestaurantsEditComponent, canActivate: [adminGuard]},
  {path: 'admin/restaurants/create', component: RestaurantsEditComponent, canActivate: [adminGuard]},

  {path: 'admin/locations', component: LocationsTableComponent, canActivate: [adminGuard]},
  {path: 'admin/locations/edit/:id', component: LocationsEditComponent, canActivate: [adminGuard]},
  {path: 'admin/locations/create', component: LocationsEditComponent, canActivate: [adminGuard]},

  {path: 'admin/product-types', component: ProductTypesTableComponent, canActivate: [adminGuard]},
  {path: 'admin/product-types/edit/:id', component: ProductTypesEditComponent, canActivate: [adminGuard]},
  {path: 'admin/product-types/create', component: ProductTypesEditComponent, canActivate: [adminGuard]},

  {path: 'admin/orders', component: OrdersTableComponent, canActivate: [adminGuard]},
  {path: 'admin/orders/edit/:id', component: OrdersEditComponent, canActivate: [adminGuard]},
  {path: 'admin/orders/create', component: OrdersEditComponent, canActivate: [adminGuard]},

  {path: 'admin/order-items', component: OrderItemsTableComponent, canActivate: [adminGuard]},
  {path: 'admin/order-items/edit/:id', component: OrderItemsEditComponent, canActivate: [adminGuard]},
  {path: 'admin/order-items/create', component: OrderItemsEditComponent, canActivate: [adminGuard]},

  {path: 'admin/prices', component: PricesTableComponent, canActivate: [adminGuard]},
  {path: 'admin/prices/edit/:id', component: PricesEditComponent, canActivate: [adminGuard]},
  {path: 'admin/prices/create', component: PricesEditComponent, canActivate: [adminGuard]},

  {path: 'admin/products', component: ProductsTableComponent, canActivate: [adminGuard]},
  {path: 'admin/products/edit/:id', component: ProductsEditComponent, canActivate: [adminGuard]},
  {path: 'admin/products/create', component: ProductsEditComponent, canActivate: [adminGuard]},
];

export default APP_ROUTES;
