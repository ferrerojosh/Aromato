import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';
import { isSuccess } from '@angular/http/src/http_utils';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  login: FormGroup;

  constructor(private authService: AuthService,
              private formBuilder: FormBuilder,
              private router: Router) {}

  ngOnInit() {
    this.login = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  onSubmit() {
    if(this.login.valid) {
      const credential = this.login.value;
      this.authService.loadDiscoveryDocument('http://localhost:5000/.well-known/openid-configuration').subscribe(() => {
        this.authService.scopes = ['roles', 'openid'];
        this.authService.login(credential.username, credential.password).subscribe(
          (isSuccess) => {
            if(isSuccess) {
              console.info('login successful');
              console.info('identity token valid: ' + this.authService.identityTokenValid());
              console.info('access token valid: ' + this.authService.accessTokenValid());

              console.info(this.authService.identityClaims);
              console.info(this.authService.accessClaims);
            }
          }
        );
      });
    }
  }

}
