import { PhotoService } from './../services/photo.service';
import { Component, OnInit } from '@angular/core';
import { ChatService, User } from '../services/chat.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  constructor(private photo:PhotoService,private chat:ChatService) { }

  async ngOnInit() {
    this.photo.GetPhoto();
    let res=await this.chat.SetCurrentUser();
  }

}
