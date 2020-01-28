import { PhotoService } from './photo.service';
import { browser } from 'protractor';
import { ConfigService } from './config.service';
import { HttpClient,HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import {CookieService} from 'node_modules/ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http:HttpClient,private config:ConfigService,private router:Router,private cookie:CookieService,private photo:PhotoService) { }

  async register(user){
    (await this.config.getConfig()).subscribe(data=>
      {
        let url=data['signup'];

        let headers = new HttpHeaders();
        headers= headers.append('content-type', 'application/json');
        this.http.post(url,JSON.stringify(user),{responseType:'text',headers:headers})
        .subscribe(
          res=>{
            this.photo.GetPhoto();
            this.router.navigate(['/chat']);
            localStorage.setItem('token',res[0]);
          },
          err=>console.log(err)
        );
      });
  }

  async signin(user){
    (await this.config.getConfig()).subscribe(data=>
      {
        let url=data['signin'];

        let headers = new HttpHeaders();
        headers= headers.append('content-type', 'application/json')
        this.http.post(url,JSON.stringify(user),{headers:headers})
        .subscribe(
          res=>{
            this.photo.GetPhoto();
            this.router.navigate(['/chat']);
            localStorage.setItem('token',res[0]);
          },
          err=>console.log(err)
        );
      });
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
