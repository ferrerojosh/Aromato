import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Store } from '@ngrx/store';

import * as fromRoot from '../../core/store/reducers';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.scss']
})
export class HomePageComponent implements OnInit {
  isLoggedIn$: Observable<boolean>;

  constructor(private store: Store<fromRoot.AppState>) { }

  ngOnInit() {
    this.isLoggedIn$ = this.store.select(fromRoot.isAuthorized);
  }

}
