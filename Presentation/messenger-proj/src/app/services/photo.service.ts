import { BehaviorSubject } from 'rxjs';
import { ConfigService } from './config.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {DomSanitizer} from '@angular/platform-browser';

@Injectable({
  providedIn: 'root'
})
export class PhotoService {

  public imageUrl:string=null;

  constructor(private http:HttpClient,private config:ConfigService,private sanitizer:DomSanitizer) { }

  async UploadPhoto(photo){
    let url = await this.config.getConfig("updatephoto");

    const uploadData = new FormData();
    uploadData.append(photo.name, photo, photo.name);

    return this.http.post(url,uploadData);
  }

  async GetPhoto(){
    let url = await this.config.getConfig("getphoto");
   
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Accept': 'application/json'
    });

    return await this.http.get(url,{responseType:"json",headers:headers}).toPromise().then(
      async res=>{
        let imgpath=await this.config.getConfig("photopath");
        this.imageUrl=`${imgpath}/${res}`;
      }) 
  }
}
