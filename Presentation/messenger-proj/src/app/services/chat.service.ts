import { PhotoService } from './photo.service';
import { ConfigService } from './config.service';
import { HttpClient ,HttpHeaders} from '@angular/common/http';
import { Injectable, OnInit, ɵConsole } from '@angular/core';
import * as signalR from "@aspnet/signalr"
import {DomSanitizer, SafeUrl} from '@angular/platform-browser';

export interface Message{
  content:string,
  userId:number,
  timeCreated:Date
}

export interface User{
  id:number
}

export interface ChatContent{
  users:User[],
  messages:Message[]
}

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  private hubConnection:signalR.HubConnection;

  public messages:Message[];

  public users:User[];

  constructor(private http:HttpClient,private config:ConfigService,private photoser:PhotoService) { }


   startConnection=async()=>{
    await this.getMessages();

    this.hubConnection = new signalR.HubConnectionBuilder()
                              .withUrl("https://localhost:44334/chat")
                              .build();
    this.hubConnection.start()
                    .then(()=>console.log("Connection started"))
                    .catch(err=>console.log(`error occured: ${err}`));                 
  }

  private async getMessages(){
    let url = await this.config.getConfig("getmessages");

    let headers = new HttpHeaders();
           headers= headers.append('content-type', 'application/json')
            
    return  this.http.get<ChatContent>(url,{headers:headers})
        .subscribe((data)=>{
          this.messages=data.messages;
          this.users=data.users;
          this.users.forEach(async u=>await this.photoser.GetUrl(u.id));
          })
 }

  public sendMessage (data:Message) {
    this.hubConnection.invoke('SendToAll', data)
    .catch(err => console.error(err));
  }

  public updateChat = () => {
    this.hubConnection.on('update', (data) => {
      this.messages.push(data);
      console.log(this.messages);
    });
}
}
