import { Injectable } from '@angular/core';

import * as fromRoot from '../store/reducers';
import { Observable } from 'rxjs/Observable';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { Store } from '@ngrx/store';

@Injectable()
export class AuthGuard implements CanActivate {

  constructor(private store: Store<fromRoot.State>,
              private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | Observable<boolean> | Promise<boolean> {
    return new Observable(observer => {
      this.store.select(fromRoot.isAuthenticated).subscribe(isLoggedIn => {
        if (!isLoggedIn) {
          this.router.navigate(['/login']);
        }

        observer.next(isLoggedIn);
        observer.complete();
      });
    });
  }

}
