import { Component, ElementRef } from '@angular/core';
import { AuthService } from '../services/auth/auth.service';
import { UserDto } from '../shared/users/models';
import { Router } from '@angular/router';

@Component({
  selector: 'app-application-layout',
  template: `<div class="container-fluid">
    <div class="row flex-nowrap" style="position: relative;">
      <div class="col-auto col-md-3 col-xl-2 px-sm-2 px-0 bg-dark sidenav">
        <div
          class="d-flex flex-column align-items-center align-items-sm-start px-3 pt-2 text-white min-vh-100"
        >
          <a
            href="/"
            class="d-flex align-items-center pb-3 mb-md-0 me-md-auto text-white text-decoration-none"
          >
          </a>
          <ul
            class="nav nav-pills flex-column mb-sm-auto mb-0 align-items-center align-items-sm-start"
            id="menu"
          >
            <li class="nav-item">
              <a routerLink="/" class="nav-link align-middle px-0">
                <i class="fa-solid fa-house text-white"></i>
                <span class="ms-3 d-none d-sm-inline text-white">Home</span>
              </a>
            </li>
            <li>
              <a routerLink="/workspaces" class="nav-link px-0 align-middle">
                <i class="fa-solid fa-briefcase text-white"></i>
                <span class="ms-3 d-none d-sm-inline text-white"
                  >Workspaces</span
                ></a
              >
            </li>
            <li>
              <a routerLink="/members" class="nav-link px-0 align-middle">
                <i class="fa-solid fa-users text-white"></i>
                <span class="ms-3 d-none d-sm-inline text-white"
                  >Members</span
                ></a
              >
            </li>
            <li>
              <a routerLink="/boards" class="nav-link px-0 align-middle">
                <i class="fa-solid fa-diagram-project text-white"></i>
                <span class="ms-3 d-none d-sm-inline text-white"
                  >Boards (temporal)</span
                ></a
              >
            </li>
            <li>
              <a routerLink="/settings" class="nav-link px-0 align-middle">
                <i class="fa-solid fa-gears text-white"></i>
                <span class="ms-3 d-none d-sm-inline text-white"
                  >Settings</span
                ></a
              >
            </li>
          </ul>
          <hr />
          <div class="dropdown pb-4">
            <a
              href="#"
              class="d-flex align-items-center text-white text-decoration-none dropdown-toggle"
              id="dropdownUser1"
              data-bs-toggle="dropdown"
              aria-expanded="false"
              style="padding-top: 20px;"
            >
              <img
                src="https://github.com/mdo.png"
                alt="hugenerd"
                width="30"
                height="30"
                class="rounded-circle"
              />
              <span class="d-none d-sm-inline mx-3" *ngIf="currentUser">{{
                currentUser.fullName
              }}</span>
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow">
              <li><a class="dropdown-item" href="#">Add project</a></li>
              <li><a class="dropdown-item" href="#">Add workspace</a></li>
              <li><a class="dropdown-item" href="#">Profile</a></li>
              <li>
                <hr class="dropdown-divider" />
              </li>
              <li>
                <a class="dropdown-item" href="#" (click)="logout()"
                  >Sign out</a
                >
              </li>
            </ul>
          </div>
        </div>
      </div>
      <div class="col main-content" style="padding: 0">
        <!-- <div class="application-layout-container">
          <nav class="application-layout-navbar navbar navbar-expand-lg">
            <div class="container-fluid d-flex align-items-center gap-3">
              <h3 style="margin: 0;">Barbers</h3>
              <div class="collapse navbar-collapse" id="navbarSupportedContent">
                <ul class="navbar-nav me-auto mb-2 mb-lg-0">
                  <li class="nav-item dropdown">
                    <p-overlayPanel #op>
                      <ng-template pTemplate="content">
                        <h4>Custom Content</h4>
                      </ng-template>
                    </p-overlayPanel>
                    <button class="btn btn-primary" (click)="op.toggle($event)">
                      <i class="fa-solid fa-filter"></i>
                      Filters
                    </button>
                  </li>
                </ul>
              </div>
            </div>
          </nav>
          <div class="application-layout-content">
            <router-outlet></router-outlet>
          </div>
        </div> -->
        <router-outlet></router-outlet>
      </div>
    </div>
  </div>`,
  styleUrls: ['application-layout.component.scss'],
})
export class ApplicationLayoutComponent {
  hasLogged = false as boolean;
  currentUser = {} as UserDto | any;
  currentGoogleUser = {};

  constructor(
    private usuarioService: AuthService,
    private router: Router,
    private elementRef: ElementRef
  ) {}

  ngOnInit(): void | any {
    this.currentUser = this.usuarioService.getCurrentUser();

    if (this.currentUser == null) {
      return this.router.navigate(['account/login']);
    }
  }

  logout(): void {
    this.usuarioService.logout().subscribe({
      complete: () => {
        this.router.navigate(['account/login']);
      },
    });
  }
}
