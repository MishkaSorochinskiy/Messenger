import { logging } from 'protractor';
import { ConfigService } from './config.service';
import { HttpClient ,HttpHeaders} from '@angular/common/http';
import { Injectable, OnInit, ɵConsole } from '@angular/core';
import * as signalR from "@aspnet/signalr"

export interface Message{
  content:string,
  timeCreated:Date
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
            
            let headers = new HttpHeaders();
            headers= headers.append('content-type', 'application/json')
             
            this.http.get<Message[]>(data["getmessages"],{headers:headers})
             .subscribe((data)=>
               {this.messages=data},
               err=>console.log(err))
           })
  }

  sendMessage(data:Message){
    let headers = new HttpHeaders();
    headers= headers.append('content-type', 'application/json');
    this.http.post("https://localhost:44334/api/Message/AddMessage",data,{responseType:'text',headers:headers})
    .subscribe(res=>console.log(res),
              err=>console.log(err));
  }

  public updateChat = () => {
    this.hubConnection.on('update', (data) => {
      this.messages.push(data);
      console.log("messages updated");
    });
}

}
