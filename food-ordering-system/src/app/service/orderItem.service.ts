import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {UserService} from "./user.service";
import {OrderItem, OrderItemRequest} from "../shared/models/OrderItem";
import {Observable} from "rxjs";
import api from "../shared/constants/api";
import {IBaseHttpApiService} from "./base/baseHttpApi.service";

@Injectable({
  providedIn: 'root'
})
export class OrderItemService implements IBaseHttpApiService<OrderItem, OrderItemRequest> {
  constructor(private http: HttpClient, private userService: UserService) {
  }

  create(OrderItem: OrderItemRequest): Observable<OrderItem> {
    return this.userService.authRequest((headers: HttpHeaders) => {
      return this.http.post<OrderItem>(api.v1.orderItems.post(), OrderItem, {headers});
    });
  }

  update(id: string, OrderItem: OrderItemRequest) {
    return this.userService.authRequest((headers: HttpHeaders) => {
      return this.http.put<OrderItem>(api.v1.orderItems.put(id), OrderItem, {headers});
    });
  }

  getAll(): Observable<OrderItem[]> {
    return this.userService.authRequest((headers: HttpHeaders) => {
      return this.http.get<OrderItem[]>(api.v1.orderItems.get(), {headers});
    });
  }

  getById(id: string): Observable<OrderItem> {
    return this.userService.authRequest(headers =>
      this.http.get<OrderItem>(api.v1.orderItems.getById(id), {headers})
    );
  }

  delete(id: string): Observable<OrderItem> {
    return this.userService.authRequest(headers =>
      this.http.delete<OrderItem>(api.v1.orderItems.delete(id), {headers})
    )
  }
}
