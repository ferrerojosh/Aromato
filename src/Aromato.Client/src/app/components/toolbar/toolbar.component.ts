import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AuthService } from '../../core/services/auth.service';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.scss']
})
export class ToolbarComponent implements OnInit {
  @Output() menuClick = new EventEmitter();

  constructor(private authService: AuthService) {
  }

  ngOnInit() {

  }

  logout() {
    this.authService.issuer = environment.authServer;
    this.authService.logout('http://localhost:4200').subscribe(redirectUri => {
      window.location.href = redirectUri;
    });
  }
}
