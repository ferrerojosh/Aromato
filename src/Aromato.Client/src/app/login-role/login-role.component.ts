import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Role } from '../role';
import { OAuthService } from 'angular-oauth2-oidc';
import { Router } from '@angular/router';
import { RoleService } from '../role.service';

@Component({
  selector: 'app-login-role',
  templateUrl: './login-role.component.html',
  styleUrls: ['./login-role.component.scss']
})
export class LoginRoleComponent implements OnInit {
  password = new FormControl();
  selectedRole = new FormControl();
  roles: Array<Role>;

  constructor(private authService: OAuthService,
              private roleService: RoleService,
              private router: Router) { }

  ngOnInit() {
    if(this.authService.hasValidAccessToken()) {
      console.info('valid access token detected');

      this.roleService.findAll().subscribe((roles) => {
        this.roles = roles;
      });
    } else {
      this.router.navigate(['/login']).then(() => {
        console.debug('not logged in! redirecting to login page...');
      });
    }
  }

  onSubmit() {
    const username = this.authService.getIdentityClaims().username;

    console.debug(this.selectedRole.value as Role);

    let role = this.selectedRole.value as Role;

    this.authService.scope = role.permissions.map(p => p.name).join(' ');
    this.authService.fetchTokenUsingPasswordFlowAndLoadUserProfile(username, this.password.value)
      .then(() => {
        console.info('logged in!')
      }).catch(() => {
      console.error('unable to login!');
    });
  }

}
