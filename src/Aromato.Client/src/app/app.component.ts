import { Component } from '@angular/core';
import { PlatformLocation } from '@angular/common';
import { Http } from '@angular/http';
import { AuthService } from './auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})

export class AppComponent {
  isAuthenticated: boolean;

  constructor(private authService: AuthService) {}

  logout() {

  }
}
