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
  photoName:string,
  id:number,
  nickname:string,
  phone:string,
  email:string,
  age:number
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

  public photourl:string;

  public currentUser:User;


  constructor(private http:HttpClient,private config:ConfigService,private sanitizer:DomSanitizer, private photo: PhotoService) { }


  startConnection=async()=>{
    this.SetCurrentUser();

    this.getMessages();

    this.photo.GetPhoto();

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
            
    return  this.http.get<ChatContent>(url,{headers:headers})
        .subscribe((data)=>{
          this.messages=data.messages;
          this.users=data.users;
          })
 }

  public sendMessage (data:Message) {
    this.hubConnection.invoke('SendToAll', data)
    .catch(err => console.error(err));
  }

  public updateChat = async () => {
    this.hubConnection.on('update', async (data) => {
      this.messages.push(data);
      if(this.users.find(u=>u.id===data.userId)==undefined){
        await this.getMessages();
      }
    });
}

  public async addUser(id:number){
    let url=`${await this.config.getConfig("getuserinfo")}?UserId=${id}`;
    let headers = new HttpHeaders();
           headers= headers.append('content-type', 'application/json')
    return this.http.get(url,{headers:headers});
  }

  public GetUrl(id:number){
      return `${this.photourl}/${this.users.find(u=>u.id===id).photoName}`; 
  }

  public GetUser(id:number){
    return this.users.find(u=>u.id===id);
  }

  public async UpdateUser() {
    let url=await this.config.getConfig("updateuser");

    let headers = new HttpHeaders();
    headers= headers.append('content-type', 'application/json');
            
    return  this.http.post(url,JSON.stringify(this.currentUser),{headers:headers}).toPromise();

  }

  public async SetCurrentUser(){
    let url=await this.config.getConfig("getuserinfo");
    
    let headers = new HttpHeaders();
    headers= headers.append('content-type', 'application/json');

    return this.http.get<User>(url,{headers:headers})
          .subscribe(res=>{this.currentUser=res; console.log(this.currentUser);});
  }

}
