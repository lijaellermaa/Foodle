import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {Order, OrderRequest} from '../shared/models/Order';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import api from "../shared/constants/api";
import {OrderStatus} from "../shared/models/enums";
import {UserService} from "./user.service";
import {IBaseHttpApiService} from "./base/baseHttpApi.service";

@Injectable({
  providedIn: 'root',
})
export class OrderService implements IBaseHttpApiService<Order, OrderRequest> {
  constructor(private http: HttpClient, private userService: UserService) {
  }

  delete(id: string): Observable<Order> {
    return this.userService.authRequest((headers: HttpHeaders) => {
      return this.http.delete<Order>(api.v1.orders.delete(id), {headers});
    });
  }

  create(order: OrderRequest): Observable<Order> {
    return this.userService.authRequest((headers: HttpHeaders) => {
      const {status, deliveryType, paymentMethod, ...value} = order;
      let newOrder = {
        status: Number(status),
        deliveryType: Number(deliveryType),
        paymentMethod: Number(paymentMethod),
        ...value
      };
      return this.http.post<Order>(api.v1.orders.post(), newOrder, {headers});
    });
  }

  update(id: string, order: OrderRequest) {
    return this.userService.authRequest((headers: HttpHeaders) => {
      const {status, deliveryType, paymentMethod, ...value} = order;
      let newOrder = {
        status: Number(status),
        deliveryType: Number(deliveryType),
        paymentMethod: Number(paymentMethod),
        ...value
      };
      return this.http.put<Order>(api.v1.orders.put(id), newOrder, {headers});
    });
  }

  getAll(): Observable<Order[]> {
    return this.userService.authRequest((headers: HttpHeaders) => {
      return this.http.get<Order[]>(api.v1.orders.get(), {headers});
    });
  }

  getById(id: string): Observable<Order> {
    return this.userService.authRequest((headers: HttpHeaders) => {
      return this.http.get<Order>(api.v1.orders.getById(id), {headers});
    });
  }

  getByUserAndStatus(userId: string, status: OrderStatus): Observable<Order[]> {
    let params = new HttpParams({
      fromObject: {
        userId,
        status
      }
    });

    return this.userService.authRequest((headers: HttpHeaders) => {
      return this.http.get<Order[]>(api.v1.orders.getByUserIdAndStatus(), {
        params,
        headers
      });
    });
  }

  payById(id: string): Observable<boolean> {
    return this.userService.authRequest((headers: HttpHeaders): Observable<boolean> => {
      return this.http.post<boolean>(api.v1.orders.postPayById(id), {}, {
        headers: headers,
      });
    });
  }
}
