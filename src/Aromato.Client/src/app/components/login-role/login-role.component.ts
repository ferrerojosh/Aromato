import { Component, OnInit } from '@angular/core';
import { routerTransition } from '../../router.animations';

@Component({
  selector: 'app-login-role',
  templateUrl: './login-role.component.html',
  styleUrls: ['./login-role.component.scss'],
  animations: [routerTransition()],
  host: {'[@routerTransition]': ''}
})
export class LoginRoleComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
