import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {UserService} from "./user.service";
import {Location, LocationRequest} from "../shared/models/Location";
import {Observable} from "rxjs";
import api from "../shared/constants/api";
import {IBaseHttpApiService} from "./base/baseHttpApi.service";

@Injectable({
  providedIn: 'root'
})
export class LocationService implements IBaseHttpApiService<Location, LocationRequest> {

  constructor(private http: HttpClient, private userService: UserService) {
  }

  create(Location: LocationRequest): Observable<Location> {
    return this.userService.authRequest((headers: HttpHeaders) => {
      return this.http.post<Location>(api.v1.locations.post(), Location, {headers});
    });
  }

  update(id: string, Location: LocationRequest) {
    return this.userService.authRequest((headers: HttpHeaders) => {
      return this.http.put<Location>(api.v1.locations.put(id), Location, {headers});
    });
  }

  getAll(): Observable<Location[]> {
    return this.userService.authRequest((headers: HttpHeaders) => {
      return this.http.get<Location[]>(api.v1.locations.get(), {headers});
    });
  }

  getById(id: string): Observable<Location> {
    return this.userService.authRequest(headers =>
      this.http.get<Location>(api.v1.locations.getById(id), {headers})
    );
  }

  delete(id: string): Observable<Location> {
    return this.userService.authRequest(headers =>
      this.http.delete<Location>(api.v1.locations.delete(id), {headers})
    )
  }
}
