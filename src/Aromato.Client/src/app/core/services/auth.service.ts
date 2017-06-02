import { Injectable } from '@angular/core';
import { Http, URLSearchParams, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';

const IDENTITY_TOKEN_STORAGE = 'identity_token';
const IDENTITY_TOKEN_CLAIMS_STORAGE = 'identity_token_claims';
const IDENTITY_TOKEN_EXPIRY = 'identity_token_expiry';
const ACCESS_TOKEN_STORAGE = 'access_token';
const ACCESS_TOKEN_CLAIMS_STORAGE = 'access_token_claims';
const ACCESS_TOKEN_EXPIRY = 'access_token_expiry';
const REFRESH_TOKEN_STORAGE = 'refresh_token';
const USER_INFO_STORAGE = 'user_info';

@Injectable()
export class AuthService {

  private issuer: string;
  public authorizationEndpoint: string;
  public tokenEndpoint: string;
  public endSessionEndpoint: string;
  public userInfoEndpoint: string;

  public supportedGrantTypes: string[];
  public supportedResponseTypes: string[];
  public supportedResponseModes: string[];
  public supportedScopes: string[];

  private store: Storage = localStorage;

  constructor(private http: Http) { }

  public init(issuer: string) {
    this.issuer = issuer;
    this.loadDiscoveryDocument();
  }

  private loadDiscoveryDocument() {
    this.http.get(this.issuer + '.well-known/openid-configuration')
      .map(r => r.json())
      .subscribe(data => {
        if (data.issuer !== this.issuer) {
          console.warn('issuer is not the same!');
        }

        this.authorizationEndpoint = data.authorization_endpoint;
        this.tokenEndpoint = data.token_endpoint;
        this.endSessionEndpoint = data.end_session_endpoint;
        this.userInfoEndpoint = data.userinfo_endpoint;

        this.supportedGrantTypes = data.grant_types_supported;
        this.supportedResponseTypes = data.response_types_supported;
        this.supportedResponseModes = data.response_modes_supported;
        this.supportedScopes = data.scopes_supported;
      },
      error => console.error('cannot load discovery document')
    );
  }

  public login(username: string, password: string, scopes: string[] = []): Observable<boolean> {
    const params = new URLSearchParams();

    params.set('grant_type', 'password');
    params.set('scope', scopes.join(' '));
    params.set('username', username);
    params.set('password', password);

    const headers = new Headers({
      'Content-Type': 'application/x-www-form-urlencoded'
    });

    return new Observable(observer =>
      this.http.post(this.tokenEndpoint, params.toString(), { headers })
      .map(r => r.json())
      .subscribe(data => {
          if (data.access_token) {
            if (data.id_token) {
              this.initializeIdentityClaims(data.id_token);
              this.saveIdentityToken(data.id_token, this.identityClaims.exp);
            }
            if (data.refresh_token) {
              this.saveRefreshToken(data.refresh_token);
            }

            this.initializeAccessClaims(data.access_token);
            this.saveAccessToken(data.access_token, this.accessClaims.exp);
            observer.next(true);
          } else {
            observer.next(false);
          }
        },
        error => observer.error(error)
      )
    );
  }

  public logout(postLogoutUri: string = '') {
    const logoutUri = this.endSessionEndpoint + `?id_token_hint=${this.identityToken}&post_logout_redirect_uri=${postLogoutUri}`;

    this.store.removeItem(ACCESS_TOKEN_EXPIRY);
    this.store.removeItem(ACCESS_TOKEN_STORAGE);
    this.store.removeItem(ACCESS_TOKEN_CLAIMS_STORAGE);
    this.store.removeItem(IDENTITY_TOKEN_EXPIRY);
    this.store.removeItem(IDENTITY_TOKEN_STORAGE);
    this.store.removeItem(IDENTITY_TOKEN_CLAIMS_STORAGE);
    this.store.removeItem(REFRESH_TOKEN_STORAGE);
    this.store.removeItem(USER_INFO_STORAGE);

    window.location.href = logoutUri;
  }

  public get authorizationHeader() {
    return 'Bearer ' + this.accessToken;
  }

  public get accessClaims() {
    return JSON.parse(this.store.getItem(ACCESS_TOKEN_CLAIMS_STORAGE));
  }

  public get identityClaims() {
    return JSON.parse(this.store.getItem(IDENTITY_TOKEN_CLAIMS_STORAGE));
  }

  public get identityToken(): string {
    return this.store.getItem(IDENTITY_TOKEN_STORAGE);
  }

  public get accessToken(): string {
    return this.store.getItem(ACCESS_TOKEN_STORAGE);
  }

  public get accessTokenValid() {
    if (this.accessToken !== null) {
      const expiry = parseInt(this.store.getItem(ACCESS_TOKEN_EXPIRY), 10);
      return Math.round(new Date().getTime() / 1000) < expiry;
    }
    return false;
  };

  public get identityTokenValid() {
    if (this.identityToken !== null) {
      const expiry = parseInt(this.store.getItem(IDENTITY_TOKEN_EXPIRY), 10);
      return Math.round(new Date().getTime() / 1000) < expiry;
    }
    return false;
  };

  private initializeAccessClaims(accessToken: string) {
    const payload = this.retrievePayload(accessToken);

    if (this.issuer !== payload.iss) {
      console.warn('wrong issuer detected');
    }

    this.store.setItem(ACCESS_TOKEN_CLAIMS_STORAGE, JSON.stringify(payload));
  }

  private initializeIdentityClaims(identityToken: string) {
    const payload = this.retrievePayload(identityToken);

    if (this.issuer !== payload.iss) {
      console.warn('wrong issuer detected');
    }

    this.store.setItem(IDENTITY_TOKEN_CLAIMS_STORAGE, JSON.stringify(payload));
  }

  private retrievePayload(jwtToken: string) {
    const jwtPartition = jwtToken.split('.');
    return JSON.parse(atob(jwtPartition[1]));
  }

  private saveRefreshToken(refreshToken: string) {
    this.store.setItem(REFRESH_TOKEN_STORAGE, refreshToken);
  }

  private saveAccessToken(accessToken: string, expiry: number) {
    this.store.setItem(ACCESS_TOKEN_EXPIRY, expiry.toString());
    this.store.setItem(ACCESS_TOKEN_STORAGE, accessToken);
  }

  private saveIdentityToken(identityToken: string, expiry: number) {
    this.store.setItem(IDENTITY_TOKEN_EXPIRY, expiry.toString());
    this.store.setItem(IDENTITY_TOKEN_STORAGE, identityToken);
  }
}
