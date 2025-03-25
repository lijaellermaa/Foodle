import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {UserService} from "./user.service";
import {ProductType, ProductTypeRequest} from "../shared/models/ProductType";
import {Observable} from "rxjs";
import api from "../shared/constants/api";
import {IBaseHttpApiService} from "./base/baseHttpApi.service";

@Injectable({
  providedIn: 'root'
})
export class ProductTypeService implements IBaseHttpApiService<ProductType, ProductTypeRequest> {

  constructor(private http: HttpClient, private userService: UserService) {
  }

  create(productType: ProductTypeRequest): Observable<ProductType> {
    return this.userService.authRequest((headers: HttpHeaders) => {
      return this.http.post<ProductType>(api.v1.productTypes.post(), productType, {headers});
    });
  }

  update(id: string, productType: ProductTypeRequest) {
    return this.userService.authRequest((headers: HttpHeaders) => {
      return this.http.put<ProductType>(api.v1.productTypes.put(id), productType, {headers});
    });
  }

  getAll(): Observable<ProductType[]> {
    return this.userService.authRequest((headers: HttpHeaders) => {
      return this.http.get<ProductType[]>(api.v1.productTypes.get(), {headers});
    });
  }

  getById(id: string): Observable<ProductType> {
    return this.userService.authRequest(headers =>
      this.http.get<ProductType>(api.v1.productTypes.getById(id), {headers})
    );
  }

  delete(id: string): Observable<ProductType> {
    return this.userService.authRequest(headers =>
      this.http.delete<ProductType>(api.v1.productTypes.delete(id), {headers})
    )
  }
}
