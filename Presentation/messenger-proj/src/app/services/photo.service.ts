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
    uploadData.append('photo', photo, photo.name);

    return this.http.post(url,uploadData);
  }

  async GetPhoto(){
    let url = await this.config.getConfig("getphoto");
   
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Accept': 'application/json'
    });
    return this.http.get(url,{responseType:"blob" as "json",headers:headers})
    .subscribe(
      async res=>{
        this.imageUrl=await this.ConvertPhotoAsync(res as Blob);
     });;
  }
  
  async ConvertPhotoAsync(data:Blob){
    return new Promise((res,rej)=>{
      var reader = new FileReader();
      reader.readAsDataURL(data as Blob); 
      reader.onloadend = ()=>{
          res(this.sanitizer.bypassSecurityTrustUrl(reader.result as string));
      }
    })
  }
}
