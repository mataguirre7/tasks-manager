import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth/auth.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard {
  constructor(private router: Router, private usuarioService: AuthService) {}

  canActivate(): boolean {
    const isLogged = this.usuarioService.getToken();
    if (!isLogged) {
      this.router.navigate(['account/login']);
      return false;
    }
    return true;
  }
}
