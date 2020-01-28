import { ChatService, Message } from './../services/chat.service';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {

  newMessage:string;

  constructor(private http:HttpClient,private chatservice:ChatService) { }

  ngOnInit() {
    this.chatservice.startConnection();
    this.chatservice.updateChat();
  }

  sendMessage(){
    this.chatservice.sendMessage({content:this.newMessage} as Message);
  }
}
