import { Component, OnInit, ViewChild } from '@angular/core';
import { MdSidenav } from '@angular/material';
import { Observable } from 'rxjs/Observable';
import { Store } from '@ngrx/store';

import * as fromRoot from '../../../core/store/reducers';
import * as layout from '../../../core/store/actions/layout';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.scss']
})
export class HomePageComponent implements OnInit {
  isSideNavOpened$: Observable<boolean>;
  @ViewChild(MdSidenav) sidenav: MdSidenav;

  constructor(private store: Store<fromRoot.AppState>) { }

  ngOnInit() {
    this.isSideNavOpened$ = this.store.select(fromRoot.isSideNavOpened);
  }

  closeSideNav() {
    this.store.dispatch(new layout.CloseSidenavAction());
  }

  openSideNav() {
    this.sidenav.open();
    this.store.dispatch(new layout.OpenSidenavAction());
  }

}
