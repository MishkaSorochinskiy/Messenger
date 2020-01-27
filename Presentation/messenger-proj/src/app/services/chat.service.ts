import { ConfigService } from './config.service';
import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit, ɵConsole } from '@angular/core';
import * as signalR from "@aspnet/signalr"

export interface Message{
  content:string,
  time:string
}

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  private hubConnection:signalR.HubConnection;

  public messages:Message[];

  constructor(private http:HttpClient,private config:ConfigService) { }


  startConnection=()=>{
    this.getMessages();

    this.hubConnection = new signalR.HubConnectionBuilder()
                              .withUrl("https://localhost:44334/chat")
                              .build();
    this.hubConnection.start()
                    .then(()=>console.log("Connection started"))
                    .catch(err=>console.log(`error occured: ${err}`));
                    
  }

  private async getMessages(){
     (await this.config.getConfig())
           .subscribe(data=>{
             this.http.get<Message[]>(data["getmessages"])
             .subscribe((data)=>{this.messages=data;console.log("hey")})
           })
  }


}
