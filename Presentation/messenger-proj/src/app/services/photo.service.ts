import { ConfigService } from './config.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {DomSanitizer} from '@angular/platform-browser';
import { ChildActivationStart } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class PhotoService {

  imageUrl;

  constructor(private http:HttpClient,private config:ConfigService,private sanitizer:DomSanitizer) { }

  async UploadPhoto(photo){
    let url = await this.config.getConfig("updatephoto");

    const uploadData = new FormData();
    console.log(photo);
    uploadData.append(photo.name, photo, photo.name);

    return this.http.post(url,uploadData);
  }

  async GetPhoto(){
    let url = await this.config.getConfig("getphoto");
   
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Accept': 'application/json'
    });

    return this.http.get(url,{responseType:"json",headers:headers}).subscribe(
      async res=>{
        let imgpath=await this.config.getConfig("photopath");
        this.imageUrl=`${imgpath}/${res}`;
      })
   
  }

   get Image(){
    this.GetPhoto();
    return this.imageUrl;
  }

}
