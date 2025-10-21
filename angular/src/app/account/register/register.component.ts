import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../../services/auth/auth.service';
import { AccountRegisterDto } from '../../shared/accounts/models';
import { AuthGoogleService } from 'src/app/services/authGoogle/auth-google.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent {
  form: FormGroup;
  inProgress: boolean;
  showPass: boolean = false;
  showConfirmPass: boolean = false;

  constructor(
    private usuarioService: AuthService,
    private toasterService: ToastrService,
    private googleAuthService: AuthGoogleService,
    private router: Router,
    private fb: FormBuilder
  ) {
    this.inProgress = false;

    this.form = this.fb.group({
      fullName: ['', Validators.required],
      email: ['', Validators.required],
      birthDate: ['', Validators.required],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required],
    });
  }

  register(): void {
    this.form.markAllAsTouched();

    if (this.form.invalid || this.inProgress) return;

    this.inProgress = true;

    let createUserDto: AccountRegisterDto = this.form.getRawValue();

    // const { fechaNacimiento } = this.form.getRawValue();

    // let convertedDate = this.convertToDateTime(fechaNacimiento);

    // createUserDto.birthDate = convertedDate;

    this.usuarioService.register(createUserDto).subscribe({
      next: (res: any) => {
        sessionStorage.setItem('token', res.token);
        sessionStorage.setItem('currentUser', JSON.stringify(res.currentUser));
      },
      complete: () => {
        this.toasterService.success('Te has registrado con éxito!', 'Usuario');
        this.router.navigate(['/']);
      },
      error: (err: any) => {
        console.error(err);
        this.toasterService.error(err.error.message, 'Usuario');
        this.inProgress = false;
      },
    });
  }

  convertToDateTime(dateString: string): Date {
    let splitedDate = dateString.split('-');
    let dateYears = parseInt(splitedDate[0]);
    let dateMonth = parseInt(splitedDate[1]) - 1;
    let dateDay = parseInt(splitedDate[2]);
    let dateTime = new Date(dateYears, dateMonth, dateDay);
    return dateTime;
  }

  toggleShowPass() {
    this.showPass = !this.showPass;
  }

  toggleShowConfirmPass() {
    this.showConfirmPass = !this.showConfirmPass;
  }
}

/*

import { Component, Injector, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { ProfileService } from '@abp/ng.account.core/proxy';
import { getPasswordValidators, ToasterService } from '@abp/ng.theme.shared';
import { comparePasswords } from '@ngx-validate/core';
import { finalize } from 'rxjs/operators';
import { ManageProfileStateService } from '@abp/ng.account';
import { ConfirmationService } from '@abp/ng.theme.shared';
import { Router } from '@angular/router';
import { ConfigStateService } from '@abp/ng.core';
import { Location } from '@angular/common';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.scss'],
})
export class ChangePasswordComponent implements OnInit {
  fieldTextType1: boolean | any;

  passwordChanged: any;

  first: boolean = false;

  fieldTextType2: boolean | any;

  fieldTextType3: boolean | any;

  message: string | any;

  inProgress = false;

  hideCurrentPassword = !this.manageProfileState.getProfile()?.hasPassword;

  PASSWORD_FIELDS = ['newPassword', 'rePassword'];

  form: FormGroup | any;
  constructor(
    private fb: FormBuilder,
    private injector: Injector,
    private toasterService: ToasterService,
    private profileService: ProfileService,
    private manageProfileState: ManageProfileStateService,
    private confirmation: ConfirmationService,
    private config: ConfigStateService,
    private router: Router,
    private location: Location,
  ) {}

  ngOnInit() {
    let property = this.config.getOne('extraProperties');
    this.passwordChanged = property['passwordChanged'];
    this.manageProfileState.getProfile$().subscribe();
    this.buildForm();
  }
  buildForm() {
    const passwordValidations = getPasswordValidators(this.injector);

    this.form = this.fb.group(
      {
        password: ['', Validators.required],
        newPassword: [
          '',
          {
            validators: [Validators.required, Validators.minLength(8), ...passwordValidations],
          },
        ],
        rePassword: [
          '',
          {
            validators: [Validators.required, Validators.minLength(8), ...passwordValidations],
          },
        ],
      },
      {
        validators: [comparePasswords(this.PASSWORD_FIELDS)],
      }
    );
  }

  toggleFieldTextType1() {
    this.fieldTextType1 = !this.fieldTextType1;
  }

  toggleFieldTextType2() {
    this.fieldTextType2 = !this.fieldTextType2;
  }

  toggleFieldTextType3() {
    this.fieldTextType3 = !this.fieldTextType3;
  }

  back() {
    this.location.back();
  }

  get passwordRequerida() {
    return (
      this.form.get('password')?.invalid &&
      this.form.get('password')?.touched &&
      this.form.get('password')?.value == ''
    );
  }

  get newPasswordRequerida() {
    return (
      this.form.get('newPassword')?.invalid &&
      this.form.get('newPassword')?.touched &&
      this.form.get('newPassword')?.value == ''
    );
  }

  get rePasswordRequerida() {
    return (
      this.form.get('rePassword')?.invalid &&
      this.form.get('rePassword')?.touched &&
      this.form.get('rePassword')?.value == ''
    );
  }

  get invalidPassword() {
    return this.form.get('password').invalid && this.form.get('password').touched;
  }

  get invalidNewPassword() {
    return this.form.get('newPassword').invalid && this.form.get('newPassword').touched;
  }

  get invalidRePassword() {
    const newPassword = this.form.get('newPassword').value;
    const rePassword = this.form.get('rePassword').value;
    if (!this.confirmPasswords(newPassword, rePassword)) return true;
    return this.form.get('rePassword').invalid && this.form.get('rePassword').touched;
  }

  get upperNewPassRequired() {
    if (!this.invalidNewPassword) return;
    return this.form.get('newPassword').errors['passwordRequiresUpper'];
  }

  get digitNewPassRequired() {
    if (!this.invalidNewPassword) return;
    return this.form.get('newPassword').errors['passwordRequiresDigit'];
  }

  get nonAlphanumericNewPassRequired() {
    if (!this.invalidNewPassword) return;
    return this.form.get('newPassword').errors['passwordRequiresNonAlphanumeric'];
  }

  get minLengthNewPassRequired() {
    if (!this.invalidNewPassword) return;
    return this.form.get('newPassword').errors['minlength'];
  }

  get newPassValidations() {
    return (
      this.nonAlphanumericNewPassRequired ||
      this.digitNewPassRequired ||
      this.upperNewPassRequired ||
      this.minLengthNewPassRequired
    );
  }

  get upperRePassRequired() {
    if (!this.invalidRePassword) return;
    return this.form.get('rePassword').errors['passwordRequiresUpper'];
  }

  get emptyPass() {
    return this.form.get('password').value == 0;
  }

  get emptyNewPass() {
    return this.form.get('newPassword').value == 0;
  }

  get emptyRePass() {
    return this.form.get('rePassword').value == 0;
  }

  get digitRePassRequired() {
    if (!this.invalidRePassword) return;
    return this.form.get('rePassword').errors['passwordRequiresDigit'];
  }

  get nonAlphanumericRePassRequired() {
    if (!this.invalidRePassword) return;
    return this.form.get('rePassword').errors['passwordRequiresNonAlphanumeric'];
  }

  get minLengthRePassRequired() {
    if (!this.invalidRePassword) return;
    return this.form.get('rePassword').errors['minlength'];
  }

  confirmPasswords(field1: string, field2: string): boolean {
    if (field1.length !== field2.length) {
      return false;
    }
    for (let i = 0; i < field1.length; i++) {
      if (field1[i] !== field2[i]) {
        return false;
      }
    }
    return true;
  }

  confirmPass() {
    const newPassword = this.form.get('newPassword').value;
    const rePassword = this.form.get('rePassword').value;
    if (!this.confirmPasswords(newPassword, rePassword)) {
      return (this.message = '::ContrasDistintas');
    }
  }

  onSubmit() {
    if (this.form.invalid || this.inProgress) return;
    console.log('Entre a submit');
    this.inProgress = true;
    this.profileService
      .changePassword({
        ...{ currentPassword: this.form.get('password').value },
        newPassword: this.form.get('newPassword').value,
      })
      .pipe(finalize(() => (this.inProgress = false)))
      .subscribe({
        next: () => {
          if (this.hideCurrentPassword) {
            this.hideCurrentPassword = false;
            this.form.addControl('password', new FormControl('', [Validators.required]));
          }
          this.toasterService.success('', 'AbpAccount::PasswordChangedMessage', {
            life: 5000,
          });
          this.buildForm();
          this.config.refreshAppState().subscribe({
            next: () => this.router.navigate(['']),
          });
        },
        error: err => {
          this.toasterService.error(err.error?.error?.message || 'AbpAccount::DefaultErrorMessage');
          this.confirmation.clear();
        },
      });
  }
}


*/
