import { Chat } from './../services/chat.service';
import { Component, OnInit } from '@angular/core';
import { ChatService } from '../services/chat.service';

@Component({
  selector: 'app-chatlist',
  templateUrl: './chatlist.component.html',
  styleUrls: ['./chatlist.component.css']
})
export class ChatlistComponent implements OnInit {

  chats:Chat[];

  constructor(private chatservice:ChatService) { }

  ngOnInit() {
    this.chatservice.GetChats();
    this.chatservice.chatssource.subscribe(res=>{
      this.chats=res;
    })
  }

}
