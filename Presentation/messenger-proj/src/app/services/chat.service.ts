import { PhotoService } from './photo.service';
import { ConfigService } from './config.service';
import { HttpClient ,HttpHeaders} from '@angular/common/http';
import { Injectable, OnInit, ɵConsole } from '@angular/core';
import * as signalR from "@aspnet/signalr"
import {DomSanitizer} from '@angular/platform-browser';
import { User } from './user.service';
import { BehaviorSubject } from 'rxjs';

export interface Message{
  content:string,
  userId:number,
  timeCreated:Date
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

  private messages=new BehaviorSubject<Message[]>([]);
  messagessource=this.messages.asObservable();

  private users=new BehaviorSubject<User[]>([]);
  userssource=this.users.asObservable();

  public photourl:string;

  messagesUpdate = this.messages.asObservable();

  constructor(private http:HttpClient,private config:ConfigService,private sanitizer:DomSanitizer, private photo: PhotoService) { }


  startConnection=async()=>{
    this.getMessages();

    this.hubConnection = new signalR.HubConnectionBuilder()
                              .withUrl("https://localhost:44334/chat")
                              .build();
    this.hubConnection.start()
                    .then(()=>console.log("Connection started"))
                    .catch(err=>console.log(`error occured: ${err}`));                 
  }

  private async getMessages(){
    let url = await this.config.getConfig("getmessages");
    
    let photopath = await this.config.getConfig("photopath");
    this.photourl=photopath;

    let headers = new HttpHeaders();
    headers= headers.append('content-type', 'application/json');
            
    return await this.http.get<ChatContent>(url,{headers:headers}).toPromise()
        .then((data)=>{
          this.MessagesUpdate(data.messages);
          this.UsersUpdate(data.users);
          })
    }

  public sendMessage (data:Message) {
    this.hubConnection.invoke('SendToAll', data)
    .catch(err => console.error(err));
  }

  public updateChat = async () => {
    this.hubConnection.on('update', async (data) => {
      
      this.messages.value.push(data);
      this.MessagesUpdate(this.messages.getValue());
      
      if(this.users.value.find(u=>u.id===data.userId)==undefined){
        await this.getMessages();
      }
    });
}

  MessagesUpdate(messages: Message[]) {
    this.messages.next(messages);
  }

  UsersUpdate(users:User[]){
    this.users.next(users);
  }

  public async CreateChate(SecondUserId:number){
    let url=await this.config.getConfig("createchat");

    let headers = new HttpHeaders();
    headers= headers.append('content-type', 'application/json');

    this.http.post(url,JSON.stringify({SecondUserId}),{headers:headers}).toPromise()
      .then(res=>console.log(res));
  }
}
