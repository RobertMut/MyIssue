import { NgModule } from '@angular/core';
import { AuthModule } from 'angular-auth-oidc-client';


@NgModule({
    imports: [AuthModule.forRoot({
        config: {
              authority: 'https://127.0.0.1:6001/connect/authorize',
              redirectUrl: 'https://localhost:5001/nav-menu-logged',
              postLogoutRedirectUri: '',
              clientId: 'webui',
              scope: 'server_api', // 'openid profile offline_access ' + your scopes
              responseType: 'code',
              silentRenew: true,
              useRefreshToken: true,
              renewTimeBeforeTokenExpiresInSeconds: 120,
          }
      })],
    exports: [AuthModule],
})
export class AuthConfigModule {}
