import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Store } from '@ngrx/store';

import * as auth from '../../core/store/actions/auth';
import * as fromRoot from '../../core/store/reducers';
import { Observable } from 'rxjs/Observable';
import { Role } from '../../core/models/role';
import { RoleService } from '../../core/services/role.service';
import { AuthService } from '../../core/services/auth.service';
import { environment } from '../../../environments/environment';
import { isAuthenticated } from '../../core/store/reducers/index';

@Component({
  selector: 'app-authorize',
  templateUrl: './authorize.component.html',
  styleUrls: ['./authorize.component.scss']
})
export class AuthorizeComponent implements OnInit {
  authorize: FormGroup;
  roles$: Observable<Role[]>;
  name: string;

  constructor(private formBuider: FormBuilder,
              private authService: AuthService,
              private roleService: RoleService,
              private store: Store<fromRoot.AppState>) { }

  ngOnInit() {
    this.store.select(fromRoot.isAuthenticated).subscribe(isAuthenticated => {
      if (!this.authService.accessTokenValid() && isAuthenticated) {
        this.authService.issuer = environment.authServer;
        this.authService.logout('http://localhost:4200').subscribe();
      }
    });

    this.name = this.authService.identityClaims().given_name;
    this.roles$ = this.roleService.findAll();
    this.authorize = this.formBuider.group({
      password: [],
      role: []
    });
  }

  onSubmit() {
    if (this.authorize.valid) {
      const creds = this.authorize.value;
      const username = this.authService.accessClaims().username;
      const selectedRole = creds.role as Role;
      this.store.dispatch(new auth.AuthorizeAction(username, creds.password, selectedRole.permissions.map(p => p.name)));
    }
  }

}
