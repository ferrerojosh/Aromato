import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';

const IDENTITY_TOKEN_STORAGE = 'identity_token';
const IDENTITY_TOKEN_EXPIRY = 'identity_token_expiry';
const ACCESS_TOKEN_STORAGE = 'access_token';
const ACCESS_TOKEN_EXPIRY = 'access_token_expiry';
const REFRESH_TOKEN_STORAGE = 'refresh_token';
const USER_INFO_STORAGE = 'user_info';

@Injectable()
export class AuthService {

  constructor(private http: Http) {}

  // open id well known configs
  public issuer: string;
  public authorizationEndpoint: string;
  public tokenEndpoint: string;
  public endSessionEndpoint: string;
  public userInfoEndpoint: string;

  public supportedGrantTypes: Array<string>;
  public supportedResponseTypes: Array<string>;
  public supportedResponseModes: Array<string>;
  public supportedScopes: Array<string>;

  public accessClaims: Map<string, any> = new Map();
  public identityClaims: Map<string, any> = new Map();

  public scopes: Array<string> = [];

  private _storage: Storage = localStorage;

  public useStorage(storage: Storage) {
    this._storage = storage;
  }

  public loadDiscoveryDocument(documentUrl: string = null): Observable<any> {
    return new Observable(observer => {
      if(documentUrl == null) {
        documentUrl = this.issuer = '.well-known/openid-configuration';
      }

      this.http.get(documentUrl).map(result => result.json()).subscribe(
        (data) => {
          this.issuer = data.issuer;
          this.authorizationEndpoint = data.authorization_endpoint;
          this.tokenEndpoint = data.token_endpoint;
          this.endSessionEndpoint = data.end_session_endpoint;
          this.userInfoEndpoint = data.user_info_endpoint;

          this.supportedGrantTypes = data.grant_types_supported;
          this.supportedResponseTypes = data.response_types_supported;
          this.supportedResponseModes = data.response_modes_supported;
          this.supportedScopes = data.scopes_supported;

          observer.next(true);
          observer.complete();
        },
        (error) => {
          console.error('cannot load discovery document');
          observer.error(error);
        }
      )
    });
  }

  public login(username: string, password: string): Observable<any> {
    return new Observable(observer => {
      let params = new URLSearchParams();

      params.set('grant_type', 'password');
      params.set('scope', this.scopes.join(' '));
      params.set('username', username);
      params.set('password', password);

      let headers = new Headers({
        'Content-Type': 'application/x-www-form-urlencoded'
      });

      this.http.post(this.tokenEndpoint, params.toString(), { headers }).map(r => r.json()).subscribe(
        (data) => {
          if(data.id_token) {
            AuthService.initializeClaims(data.id_token, this.identityClaims);
            this.saveIdentityToken(data.id_token, this.identityClaims.get('exp'));
          }
          if(data.refresh_token) {
            this.saveRefreshToken(data.refresh_token);
          }

          AuthService.initializeClaims(data.access_token, this.accessClaims);
          this.saveAccessToken(data.access_token, this.accessClaims.get('exp'));

          observer.next(true);
          observer.complete();
        },
        (error) => {
          observer.next(false);
          observer.complete();
        }
      )
    });
  }

  public userInfo(): Map<string, any> {
    return JSON.parse(this._storage.getItem(USER_INFO_STORAGE));
  }

  public identityToken(): string {
    return this._storage.getItem(IDENTITY_TOKEN_STORAGE);
  }

  public accessToken(): string {
    return this._storage.getItem(ACCESS_TOKEN_STORAGE);
  }

  public authorizationHeader() {
    return "Bearer " + this.accessToken;
  }

  accessTokenValid() {
    if (this.accessToken) {
      let expiry = parseInt(this._storage.getItem(ACCESS_TOKEN_EXPIRY));
      let now = new Date();

      return Math.round(now.getTime() / 1000) < expiry;
    }
    return false;
  };

  identityTokenValid() {
    if (this.accessToken) {
      let expiry = parseInt(this._storage.getItem(IDENTITY_TOKEN_EXPIRY));
      let now = new Date();

      return Math.round(now.getTime() / 1000) < expiry;
    }
    return false;
  };

  private retrieveUserInfo() {

  }

  private static initializeClaims(token: string, targetClaims: Map<string, any>) {
    let jwtPartition = token.split('.');
    let payload = JSON.parse(atob(jwtPartition[1]));

    for(let key in payload) {
      if(payload.hasOwnProperty(key)) {
        targetClaims.set(key, payload[key]);
      }
    }
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
