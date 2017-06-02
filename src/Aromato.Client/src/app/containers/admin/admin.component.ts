import { Component, OnInit, ViewChild } from '@angular/core';
import { Store } from '@ngrx/store';

import * as fromRoot from '../../core/store/reducers';
import * as fromLayout from '../../core/store/actions/layout';
import { Observable } from 'rxjs/Observable';
import { MdSidenav } from '@angular/material';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss'],
})
export class AdminComponent implements OnInit {
  isSideNavOpened$: Observable<boolean>;
  @ViewChild(MdSidenav) sidenav: MdSidenav;

  constructor(private store: Store<fromRoot.State>) { }

  ngOnInit() {
    this.isSideNavOpened$ = this.store.select(fromRoot.isSideNavOpened);
  }

  toggleSideNav() {
    this.sidenav.toggle().then(result => {
      const sideNavOpened = result.type === 'open';
      this.store.dispatch(new fromLayout.ToggleSidenavAction(sideNavOpened));
      localStorage.setItem('sideNavOpened', sideNavOpened.toString());
    });
  }
}
