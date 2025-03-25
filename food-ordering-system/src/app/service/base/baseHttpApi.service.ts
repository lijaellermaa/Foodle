import {Observable} from "rxjs";

export interface IBaseHttpApiService<TResponse, TRequest = TResponse, TKey = string> {
  create(entity: TRequest): Observable<TResponse>;

  update(id: TKey, entity: TRequest): Observable<TResponse>;

  getAll(): Observable<TResponse[]>;

  getById(id: TKey): Observable<TResponse>;

  delete(id: TKey): Observable<TResponse>
}
