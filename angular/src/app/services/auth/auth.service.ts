import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {
  AccountLoginDto,
  AccountRegisterDto,
} from '../../shared/accounts/models';
import { AuthGoogleService } from '../authGoogle/auth-google.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  //private readonly baseUrl = 'https://localhost:7148/api/account';
  // private readonly baseUrl = 'http://localhost:5177/api/account';
  private readonly baseUrl = 'https://tasks.nzsitsolutions.com.ar/api/account';

  constructor(
    private http: HttpClient,
    private oAuthService: AuthGoogleService
  ) {}

  register(usuarioData: AccountRegisterDto): Observable<any> {
    return this.http.post(`${this.baseUrl}/register`, usuarioData, {
      withCredentials: true,
    });
  }

  login(loginData: AccountLoginDto): Observable<any> {
    return this.http.post(`${this.baseUrl}/login`, loginData, {
      withCredentials: true,
    });
  }

  logout(): Observable<any> {
    sessionStorage.removeItem('token');
    sessionStorage.removeItem('currentUser');

    return this.http.post(`${this.baseUrl}/logout`, {
      withCredentials: true,
    });
  }

  getToken(): string {
    return sessionStorage.getItem('token') || '';
  }

  setToken(token: string): void {
    return sessionStorage.setItem('token', JSON.stringify(token));
  }

  getCurrentUser(): any {
    const currentUser = sessionStorage.getItem('currentUser')
      ? sessionStorage.getItem('currentUser')
      : this.oAuthService.getProfile();
    return typeof currentUser == 'string'
      ? JSON.parse(currentUser)
      : currentUser;
  }
}
