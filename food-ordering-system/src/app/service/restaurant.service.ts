import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {UserService} from "./user.service";
import {Restaurant, RestaurantRequest} from "../shared/models/Restaurant";
import {Observable} from "rxjs";
import api from "../shared/constants/api";
import {IBaseHttpApiService} from "./base/baseHttpApi.service";

@Injectable({
  providedIn: 'root'
})
export class RestaurantService implements IBaseHttpApiService<Restaurant, RestaurantRequest> {

  constructor(private http: HttpClient, private userService: UserService) {
  }

  create(restaurant: RestaurantRequest): Observable<Restaurant> {
    return this.userService.authRequest((headers: HttpHeaders) => {
      return this.http.post<Restaurant>(api.v1.restaurants.post(), restaurant, {headers});
    });
  }

  update(id: string, restaurant: RestaurantRequest) {
    return this.userService.authRequest((headers: HttpHeaders) => {
      return this.http.put<Restaurant>(api.v1.restaurants.put(id), restaurant, {headers});
    });
  }

  getAll(): Observable<Restaurant[]> {
    return this.userService.authRequest((headers: HttpHeaders) => {
      return this.http.get<Restaurant[]>(api.v1.restaurants.get(), {headers});
    });
  }

  getById(id: string): Observable<Restaurant> {
    return this.userService.authRequest(headers =>
      this.http.get<Restaurant>(api.v1.restaurants.getById(id), {headers})
    );
  }

  delete(id: string): Observable<Restaurant> {
    return this.userService.authRequest(headers =>
      this.http.delete<Restaurant>(api.v1.restaurants.delete(id), {headers})
    )
  }
}
