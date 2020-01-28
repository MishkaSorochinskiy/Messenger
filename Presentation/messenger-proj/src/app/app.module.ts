import { TokeninterceptorService } from './services/tokeninterceptor.service';
import { AuthGuard } from './auth.guard';
import { ConfigService } from './services/config.service';
import { AuthService } from './services/auth.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ChatComponent } from './chat/chat.component';
import { RouterModule, Routes } from '@angular/router';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { CookieService } from 'node_modules/ngx-cookie-service';
import { ProfileComponent } from './profile/profile.component';
import {DomSanitizer} from '@angular/platform-browser';

const appRoutes: Routes = [
   { path: '', redirectTo:'/chat',pathMatch:'full' },
   { path: 'chat', component:ChatComponent,canActivate:[AuthGuard]},
   { path: 'profile', component:ProfileComponent,canActivate:[AuthGuard]},
   { path: 'signin', component:LoginComponent },
   { path: 'register', component:RegisterComponent }
]

@NgModule({
   declarations: [
      AppComponent,
      NavbarComponent,
      LoginComponent,
      RegisterComponent,
      ChatComponent,
      ProfileComponent
   ],
   imports: [
      RouterModule.forRoot(appRoutes),
      BrowserModule,
      HttpClientModule,
      AppRoutingModule,
      FormsModule
   ],
   providers: [
      CookieService,
      AuthGuard,
      AuthService,
      ConfigService,
      {
         provide: HTTP_INTERCEPTORS,
         useClass: TokeninterceptorService,
         multi: true
       }
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
