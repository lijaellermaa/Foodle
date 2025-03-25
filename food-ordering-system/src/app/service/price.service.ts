import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {UserService} from "./user.service";
import {Observable} from "rxjs";
import api from "../shared/constants/api";
import {IBaseHttpApiService} from "./base/baseHttpApi.service";
import {Price, PriceRequest} from "../shared/models/Price";

@Injectable({
  providedIn: 'root'
})
export class PriceService implements IBaseHttpApiService<Price, PriceRequest> {
  constructor(private http: HttpClient, private userService: UserService) {
  }

  create(Price: PriceRequest): Observable<Price> {
    return this.userService.authRequest((headers: HttpHeaders) => {
      return this.http.post<Price>(api.v1.prices.post(), Price, {headers});
    });
  }

  update(id: string, Price: PriceRequest) {
    return this.userService.authRequest((headers: HttpHeaders) => {
      return this.http.put<Price>(api.v1.prices.put(id), Price, {headers});
    });
  }

  getAll(): Observable<Price[]> {
    return this.userService.authRequest((headers: HttpHeaders) => {
      return this.http.get<Price[]>(api.v1.prices.get(), {headers});
    });
  }

  getById(id: string): Observable<Price> {
    return this.userService.authRequest(headers =>
      this.http.get<Price>(api.v1.prices.getById(id), {headers})
    );
  }

  delete(id: string): Observable<Price> {
    return this.userService.authRequest(headers =>
      this.http.delete<Price>(api.v1.prices.delete(id), {headers})
    )
  }
}
