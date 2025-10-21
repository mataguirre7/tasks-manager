import { Component, OnInit } from '@angular/core';
import { UserDto } from './shared/users/models';
import { AuthService } from './services/auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  template: `<router-outlet></router-outlet>`,
})
export class AppComponent implements OnInit {
  hasLogged = false as boolean;
  currentUser = {} as UserDto | any;
  currentGoogleUser = {};

  constructor(private usuarioService: AuthService, private router: Router) {}

  ngOnInit(): void | any {
    this.currentUser = this.usuarioService.getCurrentUser();

    if (this.currentUser == null) {
      return this.router.navigate(['account/login']);
    }
  }
}
