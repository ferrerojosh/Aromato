import { Component, OnInit } from '@angular/core';
import { MdDialogRef, MdSnackBar } from '@angular/material';
import { Role } from '../../core/models/role';
import { RoleService } from '../../core/services/role.service';
import { Observable } from 'rxjs/Observable';
import { AuthService } from '../../core/services/auth.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';

import * as auth from '../../core/store/actions/auth';
import * as fromRoot from '../../core/store/reducers';
import { Store } from '@ngrx/store';

@Component({
  selector: 'app-authorize',
  templateUrl: './authorize.component.html',
  styleUrls: ['./authorize.component.scss']
})
export class AuthorizeComponent implements OnInit {
  authorizeForm: FormGroup;
  roles$: Observable<Role[]>;
  name: string;

  constructor(public dialogRef: MdDialogRef<AuthorizeComponent>,
              private authService: AuthService,
              private formBuilder: FormBuilder,
              private router: Router,
              public snackBar: MdSnackBar,
              private store: Store<fromRoot.AppState>,
              private roleService: RoleService) { }

  ngOnInit() {
    this.authorizeForm = this.formBuilder.group({
      password: [''],
      selectedRole: ['']
    });
    this.name = this.authService.identityClaims().given_name;
    this.roles$ = this.roleService.findAll();
  }

  onSubmit() {
    let username = this.authService.identityClaims().username;
    let password = this.authorizeForm.value.password;
    let selectedRole = this.authorizeForm.value.selectedRole;

    if (this.authorizeForm.valid) {
      this.authService.login(username, password, selectedRole.permissions.map(p => p.name)).subscribe(() => {
        this.store.dispatch(new auth.AuthorizeAction(this.authService.accessToken()));
        this.router.navigate(['/']);
        this.dialogRef.close();
      }, error => {
        this.dialogRef.close();
        this.snackBar.open('There was an error authorizing selected role. Please check your internet connection.');
      });
    }

  }

}
