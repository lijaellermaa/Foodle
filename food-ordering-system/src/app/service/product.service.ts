import {Injectable} from '@angular/core';
import {Product, ProductRequest} from '../shared/models/Product';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs';
import api from "../shared/constants/api";
import {FilterQuery} from "../shared/models/FilterQuery";
import {IBaseHttpApiService} from "./base/baseHttpApi.service";
import {UserService} from "./user.service";

@Injectable({
  providedIn: 'root',
})
export class ProductService implements IBaseHttpApiService<Product, ProductRequest> {
  constructor(private http: HttpClient, private userService: UserService) {
  }

  create(entity: ProductRequest): Observable<Product> {
    return this.userService.authRequest((headers: HttpHeaders) => {
      return this.http.post<Product>(api.v1.products.post(), entity, {headers});
    });
  }

  update(id: string, entity: ProductRequest): Observable<Product> {
    return this.userService.authRequest((headers: HttpHeaders) => {
      return this.http.put<Product>(api.v1.products.put(id), entity, {headers});
    });
  }

  delete(id: string): Observable<Product> {
    return this.userService.authRequest(headers =>
      this.http.delete<Product>(api.v1.products.delete(id), {headers})
    )
  }

  getAll(restaurantId: string | undefined = undefined, filterQuery: FilterQuery | undefined = undefined): Observable<Product[]> {
    let params = new HttpParams();

    if (restaurantId !== undefined) params = params.append('restaurantId', restaurantId);
    if (filterQuery?.limit !== undefined) params = params.append('Limit', filterQuery.limit);
    if (filterQuery?.offset !== undefined) params = params.append('Offset', filterQuery.offset);
    if (filterQuery?.sortBy !== undefined) params = params.append('SortBy', filterQuery.sortBy);
    if (filterQuery?.searchQuery !== undefined) params = params.append('SearchQuery', filterQuery.searchQuery);

    return this.http.get<Product[]>(api.v1.products.get(), {params});
  }

  getById(productId: string): Observable<Product> {
    return this.http.get<Product>(api.v1.products.getById(productId));
  }
}
