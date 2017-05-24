import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';

import * as fromRoot from '../../core/store/reducers';
import * as auth from '../../core/store/actions/auth';
import { Observable } from 'rxjs/Observable';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss']
})
export class LoginPageComponent implements OnInit {
  login: FormGroup;
  isAuthenticated$: Observable<boolean>;

  constructor(private formBuilder: FormBuilder,
              private router: Router,
              private store: Store<fromRoot.AppState>) { }

  ngOnInit() {
    this.store.select(fromRoot.isAuthorized).subscribe(isAuthorized => {
      if (isAuthorized) {
        this.router.navigate(['/admin']);
      }
    });
    this.login = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });

    this.isAuthenticated$ = this.store.select(fromRoot.isAuthenticated);
  }

  onSubmit() {
    if (this.login.valid) {
      const credentials = this.login.value;
      this.store.dispatch(new auth.AuthenticateAction(credentials.username, credentials.password, ['openid', 'roles']));
    }

  }

}
