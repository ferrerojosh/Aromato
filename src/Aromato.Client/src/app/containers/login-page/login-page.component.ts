import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../core/services/auth.service';
import { Store } from '@ngrx/store';

import * as fromRoot from '../../core/store/reducers';
import * as auth from '../../core/store/actions/auth';
import { MdDialog, MdDialogRef, MdSnackBar } from '@angular/material';
import { AuthorizeComponent } from '../../components/authorize/authorize.component';
import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss']
})
export class LoginPageComponent implements OnInit {
  loginForm: FormGroup;

  constructor(private formBuilder: FormBuilder,
              private authService: AuthService,
              public dialog: MdDialog,
              public snackBar: MdSnackBar,
              private store: Store<fromRoot.AppState>) { }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  onSubmit() {
    this.snackBar.dismiss();
    if (this.loginForm.valid) {
      const credentials = this.loginForm.value;

      this.authService.issuer = 'http://localhost:5000/';
      this.authService.login(credentials.username, credentials.password, ['openid', 'roles']).subscribe(() => {
        this.store.dispatch(new auth.AuthenticateAction(this.authService.identityClaims(), this.authService.accessToken()));
        this.dialog.open(AuthorizeComponent, {
          height: '550px',
          width: '400px',
        });
      }, error => {
        this.snackBar.open('There was an error logging you in. Please check your internet connection.');
      });
    }
  }

}
