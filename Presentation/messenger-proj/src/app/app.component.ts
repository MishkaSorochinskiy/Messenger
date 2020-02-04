import { PhotoService } from './services/photo.service';
import { Component, OnInit } from '@angular/core';
import { User, UserService } from './services/user.service';
import { AuthGuard } from './auth.guard';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent{
  title = 'messenger-proj';

  constructor(private userservice:UserService,private guard:AuthGuard) {
    if(this.guard.canActivate()){
      this.userservice.SetCurrentUser();
    }
  }
}
