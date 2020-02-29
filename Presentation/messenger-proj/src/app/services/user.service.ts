import { ChatService } from './chat.service';
import { PhotoService } from './photo.service';
import { ConfigService } from './config.service';
import { HttpClient,HttpHeaders } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import {BehaviorSubject} from 'rxjs';

export class User{
  id:number;
  photoName:string;
  nickName:string;
  phone:string;
  email:string;
  age:number;
  isblocked:boolean
}

@Injectable({
  providedIn: 'root'
})
export class UserService  {

  public currentUser=new BehaviorSubject<User>(new User());
  data=this.currentUser.asObservable();

  private searchUsers=new BehaviorSubject<User[]>([]);
  searchdata=this.searchUsers.asObservable();

  valid:boolean=false;

  updated:boolean=false;

  constructor(private http:HttpClient,private config:ConfigService,private photoservice:PhotoService,private chatservice:ChatService) { }

  public async UpdateUser(data) {
    let url=await this.config.getConfig("updateuser");

    let headers = new HttpHeaders();
    headers= headers.append('content-type', 'application/json');
            
    return this.http.post(url,JSON.stringify(data),{headers:headers}).subscribe(
      ()=>{
        this.updateCurrentUser(data);
        this.valid=false;
        this.updated=true;
      },
      error=>{
        this.valid=true;
      }
    );
  }

  public async SetCurrentUser(){
    let url=await this.config.getConfig("getuserinfo");
    let imgpath=await this.config.getConfig("photopath");
    
    let headers = new HttpHeaders();
    headers= headers.append('content-type', 'application/json');

    return await this.http.get<User>(url,{headers:headers}).toPromise()
    .then( res=>
      {
        res.photoName=`${imgpath}/${res.photoName}`;
        this.updateCurrentUser(res);
      });
  }

  updateCurrentUser(user:User){
    this.currentUser.next(user);
  }

  updateSearchUsers(values:User[]){
    this.searchUsers.next(values);
  }

  public async SearchUsers(filter:string){
    let url =await this.config.getConfig("search")+`?Filter=${filter}`;
    let imgpath=await this.config.getConfig("photopath");
    return await this.http.get<User[]>(url).toPromise()
    .then(res=>
      {
       let mappedres= res.map(user=>{
          user.photoName=`${imgpath}/${user.photoName}`;
          return user;
        })

        this.updateSearchUsers(mappedres);
        return mappedres;
      });
  }

 async block(id:number){
    let url=await this.config.getConfig("blockuser");

    let headers = new HttpHeaders();
    headers= headers.append('content-type', 'application/json');
            
    return this.http.post(url,JSON.stringify({UserIdToBlock:id}),{headers:headers}).toPromise()
    .then(()=>this.chatservice.UpdateChats());
  }

 async unblock(id:number){
    let url=await this.config.getConfig("unblockuser");
    let headers = new HttpHeaders();
    headers= headers.append('content-type', 'application/json');
            
    return this.http.post(url,JSON.stringify({UserIdToBlock:id}),{headers:headers}).toPromise()
    .then(()=>this.chatservice.UpdateChats());
  }
}
