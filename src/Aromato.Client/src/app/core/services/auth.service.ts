import { Injectable } from '@angular/core';
import { Http, Headers, URLSearchParams } from '@angular/http';
import { Observable } from 'rxjs/Observable';

export const IDENTITY_TOKEN_STORAGE = 'identity_token';
export const IDENTITY_TOKEN_CLAIMS_STORAGE = 'identity_token_claims';
export const IDENTITY_TOKEN_EXPIRY = 'identity_token_expiry';
export const ACCESS_TOKEN_STORAGE = 'access_token';
export const ACCESS_TOKEN_CLAIMS_STORAGE = 'access_token_claims';
export const ACCESS_TOKEN_EXPIRY = 'access_token_expiry';
export const REFRESH_TOKEN_STORAGE = 'refresh_token';
export const USER_INFO_STORAGE = 'user_info';

@Injectable()
export class AuthService {

  // open id well known configs
  public issuer: string;
  public authorizationEndpoint: string;
  public tokenEndpoint: string;
  public endSessionEndpoint: string;
  public userInfoEndpoint: string;

  public supportedGrantTypes: string[];
  public supportedResponseTypes: string[];
  public supportedResponseModes: string[];
  public supportedScopes: string[];

  public scopes: string[] = [];

  private _storage: Storage = localStorage;

  constructor(private http: Http) {}

  public useStorage(storage: Storage) {
    this._storage = storage;
  }

  public loadDiscoveryDocument(documentUrl: string = null): Observable<any> {
    return new Observable(observer => {
      if (documentUrl == null) {
        documentUrl = this.issuer + '.well-known/openid-configuration';
      }

      this.http.get(documentUrl).map(result => result.json()).subscribe(
        data => {
          this.issuer = data.issuer;
          this.authorizationEndpoint = data.authorization_endpoint;
          this.tokenEndpoint = data.token_endpoint;
          this.endSessionEndpoint = data.end_session_endpoint;
          this.userInfoEndpoint = data.userinfo_endpoint;

          this.supportedGrantTypes = data.grant_types_supported;
          this.supportedResponseTypes = data.response_types_supported;
          this.supportedResponseModes = data.response_modes_supported;
          this.supportedScopes = data.scopes_supported;

          observer.next(true);
          observer.complete();
        },
        error => {
          console.error('cannot load discovery document');
          observer.error(error);
        }
      );
    });
  }

  public login(username: string, password: string, scopes: Array<string> = null): Observable<boolean> {
    return new Observable(observer => {
      this.loadDiscoveryDocument().subscribe(() => {
        const params = new URLSearchParams();

        if (scopes != null) {
          this.scopes = scopes;
        }

        params.set('grant_type', 'password');
        params.set('scope', this.scopes.join(' '));
        params.set('username', username);
        params.set('password', password);

        const headers = new Headers({
          'Content-Type': 'application/x-www-form-urlencoded'
        });

        this.http.post(this.tokenEndpoint, params.toString(), { headers }).map(r => r.json()).subscribe(
          data => {
            if (data.id_token) {
              this.initializeIdentityClaims(data.id_token);
              this.saveIdentityToken(data.id_token, this.identityClaims().exp);
            }
            if (data.refresh_token) {
              this.saveRefreshToken(data.refresh_token);
            }

            this.initializeAccessClaims(data.access_token);
            this.saveAccessToken(data.access_token, this.accessClaims().exp);
            this.retrieveUserInfo().subscribe();

            observer.next(true);
            observer.complete();
          },
          error => {
            observer.error(error);
          }
        );
      },
      error => {
        observer.error(error);
      });
    });
  }

  public userInfo(): Map<string, any> {
    if (!this.userInfoEndpoint) {
      throw new Error('user info endpoint not supported by auth server');
    }
    return JSON.parse(this._storage.getItem(USER_INFO_STORAGE));
  }

  public identityToken(): string {
    return this._storage.getItem(IDENTITY_TOKEN_STORAGE);
  }

  public accessToken(): string {
    return this._storage.getItem(ACCESS_TOKEN_STORAGE);
  }

  public logout(postLogoutUri: string = null): Observable<string> {
    return new Observable(observer => {
      this.loadDiscoveryDocument().subscribe(() => {
        const logoutUri = this.endSessionEndpoint + `?id_token_hint=${this.identityToken()}&post_logout_redirect_uri=${postLogoutUri}`;

        this._storage.removeItem(ACCESS_TOKEN_EXPIRY);
        this._storage.removeItem(ACCESS_TOKEN_STORAGE);
        this._storage.removeItem(ACCESS_TOKEN_CLAIMS_STORAGE);
        this._storage.removeItem(IDENTITY_TOKEN_EXPIRY);
        this._storage.removeItem(IDENTITY_TOKEN_STORAGE);
        this._storage.removeItem(IDENTITY_TOKEN_CLAIMS_STORAGE);
        this._storage.removeItem(REFRESH_TOKEN_STORAGE);
        this._storage.removeItem(USER_INFO_STORAGE);

        observer.next(logoutUri);
      });
    });
  }

  public authorizationHeader() {
    return 'Bearer ' + this.accessToken();
  }

  public accessClaims() {
    return JSON.parse(this._storage.getItem(ACCESS_TOKEN_CLAIMS_STORAGE));
  }

  public identityClaims() {
    return JSON.parse(this._storage.getItem(IDENTITY_TOKEN_CLAIMS_STORAGE));
  }

  public accessTokenValid() {
    if (this.accessToken() !== null) {
      const expiry = parseInt(this._storage.getItem(ACCESS_TOKEN_EXPIRY), 10);
      return Math.round(new Date().getTime() / 1000) < expiry;
    }
    return false;
  };

  public identityTokenValid() {
    if (this.identityToken() !== null) {
      const expiry = parseInt(this._storage.getItem(IDENTITY_TOKEN_EXPIRY), 10);
      return Math.round(new Date().getTime() / 1000) < expiry;
    }
    return false;
  };

  private retrieveUserInfo(): Observable<boolean> {
    return new Observable(observer => {
      if (this.userInfoEndpoint) {
        const headers = new Headers({
          'Authorization': this.authorizationHeader()
        });

        this.http.get(this.userInfoEndpoint, { headers }).map(r => r.json()).subscribe(
          userInfo => {
            this._storage.setItem(USER_INFO_STORAGE, JSON.stringify(userInfo));
            observer.next(true);
          },
          error => {
            observer.error(error);
          }
        );
      } else {
        console.warn('user info endpoint not supported');
        observer.next(false);
      }
      observer.complete();
    });
  }

  private initializeAccessClaims(accessToken: string) {
    const payload = AuthService.retrievePayload(accessToken);

    if (this.issuer !== payload.iss) {
      console.warn('wrong issuer detected');
    }

    this._storage.setItem(ACCESS_TOKEN_CLAIMS_STORAGE, JSON.stringify(payload));
  }

  private initializeIdentityClaims(identityToken: string) {
    const payload = AuthService.retrievePayload(identityToken);

    if (this.issuer !== payload.iss) {
      console.warn('wrong issuer detected');
    }

    this._storage.setItem(IDENTITY_TOKEN_CLAIMS_STORAGE, JSON.stringify(payload));
  }

  private static retrievePayload(jwtToken: string) {
    const jwtPartition = jwtToken.split('.');
    return JSON.parse(atob(jwtPartition[1]));
  }

  private saveRefreshToken(refreshToken: string) {
    this._storage.setItem(REFRESH_TOKEN_STORAGE, refreshToken);
  }

  private saveAccessToken(accessToken: string, expiry: number) {
    this._storage.setItem(ACCESS_TOKEN_EXPIRY, expiry.toString());
    this._storage.setItem(ACCESS_TOKEN_STORAGE, accessToken);
  }

  private saveIdentityToken(identityToken: string, expiry: number) {
    this._storage.setItem(IDENTITY_TOKEN_EXPIRY, expiry.toString());
    this._storage.setItem(IDENTITY_TOKEN_STORAGE, identityToken);
  }
}
