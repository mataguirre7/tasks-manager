import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../../services/auth/auth.service';
import { AccountLoginDto } from '../../shared/accounts/models';
import { AuthGoogleService } from 'src/app/services/authGoogle/auth-google.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  form: FormGroup;
  inProgress: boolean;
  showPass: boolean;

  constructor(
    private usuarioService: AuthService,
    private router: Router,
    private toasterService: ToastrService,
    private fb: FormBuilder,
    private googleAuthService: AuthGoogleService
  ) {
    this.inProgress = false;

    this.form = this.fb.group({
      email: ['', Validators.required],
      password: ['', Validators.required],
      // rememberMe: [false],
    });

    this.showPass = false;
  }

  login(): void {
    this.form.markAllAsTouched();

    if (this.form.invalid || this.inProgress) return;

    this.inProgress = true;

    let loginUserDto: AccountLoginDto = this.form.getRawValue();

    this.usuarioService.login(loginUserDto).subscribe({
      next: (res) => {
        sessionStorage.setItem('token', res.token);
        sessionStorage.setItem('currentUser', JSON.stringify(res.currentUser));
      },
      complete: () => {
        this.toasterService.success(
          'Has iniciado sesión con éxito!',
          'Usuario'
        );
        this.router.navigate(['/']);
      },
      error: (err) => {
        console.error(err);
        this.toasterService.error(err.error.message, 'Usuario');
        this.inProgress = false;
      },
    });
  }

  toggleShowPass() {
    this.showPass = !this.showPass;
  }

  toRegister() {
    this.router.navigate(['account/register']);
  }
}
