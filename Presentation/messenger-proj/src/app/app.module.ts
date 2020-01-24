import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ChatComponent } from './chat/chat.component';
import { RouterModule, Routes } from '@angular/router';

const appRoutes: Routes = [
   { path: '', redirectTo:'/chat',pathMatch:'full' },
   { path: 'chat', component:ChatComponent },
   { path: 'signin', component:LoginComponent },
   { path: 'register', component:RegisterComponent }
]

@NgModule({
   declarations: [
      AppComponent,
      NavbarComponent,
      LoginComponent,
      RegisterComponent,
      ChatComponent
   ],
   imports: [
      RouterModule.forRoot(appRoutes),
      BrowserModule,
      AppRoutingModule
   ],
   providers: [],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
