import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth/auth.service';

@Injectable({
  providedIn: 'root',
})
export class LoggedGuard {
  constructor(private router: Router, private usuarioService: AuthService) {}

  canActivate(): any {
    const isLogged = this.usuarioService.getToken();
    if (isLogged) {
      this.router.navigate(['/']);
      return false;
    }
    return true;
  }
}
