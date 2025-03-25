import {Injectable} from '@angular/core';
import {BehaviorSubject, Observable, tap} from 'rxjs';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {ToastrService} from 'ngx-toastr';
import {AuthResponse, ChangeProfileRequest, LoginRequest, RegisterRequest, UserPayload} from '../shared/models/Auth';
import {JwtHelperService} from '@auth0/angular-jwt';
import api from "../shared/constants/api";
import {ErrorResponse} from "../shared/models/Error";
import {USER_SESSION_KEYS} from "../shared/constants/session";

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private jwtHelper = new JwtHelperService();
  private _session = new BehaviorSubject<AuthResponse | undefined>(undefined);

  constructor(private http: HttpClient, private toastrService: ToastrService) {
  }

  public get session$() {
    return this._session.asObservable();
  }

  public hasRole(roleId: string): boolean {
    const session = this.getSession();
    if (!session) return false;
    return session.roles.includes(roleId);
  }

  public get isAdmin(): boolean {
    return this.hasRole("Admin")
  }

  public get currentSession(): AuthResponse | undefined {
    return this.getSession();
  }

  public get currentUser(): UserPayload | undefined {
    return this.getSession()?.user;
  }

  public get userFullName(): string | undefined {
    const session = this.getSession();
    if (!session) {
      return;
    }

    return `${session.user.firstName ?? ''} ${session.user.lastName ?? ''}`;
  }

  public login(userLogin: LoginRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(api.v1.auth.login(), userLogin).pipe(
      tap({
        next: (user) => {
          this.setSession(user);
          this.toastrService.success(`Welcome to Foodle!`, 'Login Successful');
        },
        error: (errorResponse) => {
          this.toastrService.error(errorResponse.error, 'Login Failed');
        },
      })
    );
  }

  public register(userRegister: RegisterRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(api.v1.auth.register(), userRegister).pipe(
      tap({
        next: (res) => {
          this.setSession(res);
          this.toastrService.success(
            `Welcome to the Foodle ${res.user.firstName ?? "Guest"}`,
            'Register Successful'
          );
        },
        error: (errorResponse) => {
          this.toastrService.error(errorResponse.error, 'Register Failed');
        },
      })
    );
  }

  public logout() {
    localStorage.removeItem(USER_SESSION_KEYS.session);
    this._session.next(undefined);
    window.location.reload();
  }

  public authRequest<T>(
    fn: (headers: HttpHeaders) => Observable<T>,
  ): Observable<T> {
    const session = this.getSession();
    const headers = session ? new HttpHeaders({
      "Authorization": `Bearer ${session?.token}`,
    }) : new HttpHeaders({})

    return fn(headers);
  }

  public changeProfile(user: ChangeProfileRequest): Observable<AuthResponse> {
    const session = this.getSession();
    let headers = new HttpHeaders({
      "Authorization": `Bearer ${session?.token}`,
    });
    return this.http
      .post<AuthResponse>(api.v1.auth.changeProfile(), user, {headers})
      .pipe(
        tap({
          next: (user) => {
            this.setSession(user);
            this.toastrService.success('', 'Profile changed successfully');
          },
          error: (errorResponse: ErrorResponse) => {
            this.toastrService.error(
              errorResponse.error,
              'Profile Change Failed'
            );
          },
        })
      );
  }

  public get isAuthenticated(): boolean {
    const session = this.getSession();
    return session?.token !== undefined && this.checkToken(session.token);
  }

  private checkToken(token: string): boolean {
    return !this.jwtHelper.isTokenExpired(token);
  }

  private getSession(): AuthResponse | undefined {
    const sessionString = localStorage.getItem(USER_SESSION_KEYS.session)
    if (!sessionString) {
      return;
    }
    const session = JSON.parse(sessionString) as AuthResponse | undefined
    if (!session || !this.checkToken(session?.token)) {
      this.logout();
      this._session.next(undefined);
      return;
    }
    this._session.next(session);

    return session;
  }

  private setSession(authResponse: AuthResponse) {
    this._session.next(authResponse);
    localStorage.setItem(USER_SESSION_KEYS.session, JSON.stringify(authResponse));
  }
}
