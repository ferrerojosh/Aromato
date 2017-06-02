import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';

import * as fromRoot from '../../core/store/reducers';
import * as auth from '../../core/store/actions/auth';
import * as role from '../../core/store/actions/role';

import { Observable } from 'rxjs/Observable';
import { Role } from '../../core/models/role';
import { go } from '@ngrx/router-store';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss']
})
export class LoginPageComponent implements OnInit, OnDestroy {
  loginForm: FormGroup;
  areRolesLoaded$: Observable<boolean>;
  areRolesLoading$: Observable<boolean>;
  isAuthLoading$: Observable<boolean>;
  isAuthenticated$: Observable<boolean>;
  authError$: Observable<boolean>;
  roles$: Observable<Role[]>;

  private alive = true;

  constructor(private formBuilder: FormBuilder,
              private store: Store<fromRoot.State>) {}

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      username: ['ferrerojosh', [Validators.required]],
      password: ['', [Validators.required, Validators.minLength(8)]],
      selectedRole: []
    });

    // subscribe to loaded roles
    this.areRolesLoaded$ = this.store.select(fromRoot.areRolesLoaded);
    this.areRolesLoading$ = this.store.select(fromRoot.areRolesLoading);
    this.isAuthLoading$ = this.store.select(fromRoot.isAuthLoading);
    this.isAuthenticated$ = this.store.select(fromRoot.isAuthenticated);
    this.roles$ = this.store.select(fromRoot.getRoles);
    this.authError$ = this.store.select(fromRoot.onAuthError);

    this.isAuthenticated$
      .takeWhile(() => this.alive)
      .filter(authenticated => authenticated)
      .subscribe(() => this.store.dispatch(go('admin')));
  }

  public ngOnDestroy() {
    this.alive = false;
  }

  checkRole() {
    this.store.dispatch(new role.LoadAction(this.loginForm.get('username').value));
  }

  login() {
    if (this.loginForm.valid) {
      const credentials = this.loginForm.value;
      const role = credentials.selectedRole as Role;
      this.store.dispatch(new auth.AuthenticateAction(credentials.username, credentials.password, role.permissions
        .map(p => p.name)));
    }
  }

}
