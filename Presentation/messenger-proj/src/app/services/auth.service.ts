import { User } from './chat.service';
import { PhotoService } from './photo.service';
import { browser } from 'protractor';
import { ConfigService } from './config.service';
import { HttpClient,HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import {CookieService} from 'node_modules/ngx-cookie-service';

export class UserInfo{
  Email:string;
  Password:string;
  PasswordConfirm:string;
  NickName:string;
  Sex:number;
  PhoneNumber:string;
  Age:number;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  userInfo:UserInfo=new UserInfo();

  constructor(private http:HttpClient,private config:ConfigService,private router:Router,private cookie:CookieService,private photo:PhotoService) {}

  async register(){
    let url=await this.config.getConfig("signup");

    let headers = new HttpHeaders();
    headers= headers.append('content-type', 'application/json');
    
    return this.http.post(url,JSON.stringify(this.userInfo),{responseType:'text',headers:headers})
        .subscribe(
          res=>{
            this.photo.GetPhoto();
            this.router.navigate(['/chat']);
            localStorage.setItem('token',res[0]);
          },err=>console.log(err));
  }

  async signin(user){
    let url=await this.config.getConfig("signin");
    
    let headers = new HttpHeaders();
    headers= headers.append('content-type', 'application/json');
    
    return this.http.post(url,JSON.stringify(user),{responseType:'text',headers:headers})
          .subscribe(res=>{
            this.photo.GetPhoto();
            localStorage.setItem('token',res);
            this.router.navigate(['/chat']);
          });
  }

  async fillRegister(data){
    let url=await this.config.getConfig("checkemail");
    this.userInfo.Email=data.email;
    this.userInfo.Password=data.password;
    this.userInfo.PasswordConfirm=data.passwordconfirm;

    let headers = new HttpHeaders();
    headers= headers.append('content-type', 'application/json');
    
    return this.http.post<boolean>(url,this.userInfo,{headers:headers}).toPromise();
  }

  fillInfo(data){
    this.userInfo.NickName=data.NickName;
    this.userInfo.Sex= parseInt(data.Sex);
    this.userInfo.PhoneNumber=data.PhoneNumber;
    this.userInfo.Age=data.Age;
    this.register();
  }
  


  signout(){
    this.router.navigate(['/signin']);
    localStorage.removeItem('token');
  }

  looggedIn(){
    return !!localStorage.getItem('token');
  }

  gettoken(){
    return localStorage.getItem('token');
  }
}
