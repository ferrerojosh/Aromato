import { Component, ViewChild } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Store } from '@ngrx/store';

import * as fromRoot from './core/store/reducers';
import * as layout from './core/store/actions/layout';
import * as auth from './core/store/actions/auth';
import { MdSidenav } from '@angular/material';
import { AuthService } from './core/services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  showSidenav$: Observable<boolean>;
  isLoggedIn$: Observable<boolean>;
  @ViewChild(MdSidenav) sidenav: MdSidenav;

  constructor(private store: Store<fromRoot.AppState>,
              private authService: AuthService) {
    this.showSidenav$ = this.store.select(fromRoot.showSidenav);
    this.isLoggedIn$ = this.store.select(fromRoot.isAuthorized);
  }

  openSidenav() {
    this.sidenav.open();
    this.store.dispatch(new layout.OpenSidenavAction());
  }

  logout() {
    this.authService.logout('http://localhost:4200/').subscribe(logoutUri => {
      this.store.dispatch(new auth.LogoutAction());
      window.location.href = logoutUri;
    });
  }

  closeSidenav() {
    this.store.dispatch(new layout.CloseSidenavAction());
  }
}
