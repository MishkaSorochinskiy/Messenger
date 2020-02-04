import { PhotoService } from './../services/photo.service';
import { Component, OnInit } from '@angular/core';
import { UserService, User } from '../services/user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  currentUser:User=new User();


  constructor(private photo:PhotoService,private userservice:UserService) { }

 ngOnInit() {
    this.userservice.data.subscribe(user=>this.currentUser=user);
  }

  UpdateUser(){
    this.userservice.UpdateUser(this.currentUser)
      .then(()=>this.userservice.updateCurrentUser(this.currentUser));
  }

}
