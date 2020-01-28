import { ConfigService } from './config.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {DomSanitizer} from '@angular/platform-browser';

@Injectable({
  providedIn: 'root'
})
export class PhotoService {

  imageUrl;

  constructor(private http:HttpClient,private config:ConfigService,private sanitizer:DomSanitizer) { }

  async UploadPhoto(photo){
    (await this.config.getConfig())
        .subscribe(data=>{
          let url=data["updatephoto"];
          
          const uploadData = new FormData();
          uploadData.append('photo', photo, photo.name);
          this.http.post(url,uploadData).subscribe(
            res=>this.GetPhoto(),
            err=>console.log(err));
        })
  }

  async GetPhoto(){
    (await this.config.getConfig())
    .subscribe(data=>{
      let url=data["getphoto"];
     
      const headers = new HttpHeaders({
        'Content-Type': 'application/json',
        'Accept': 'application/json'
      });

      this.http.get(url,{responseType:"blob" as "json",headers:headers}).subscribe(
        res=>{
          var reader = new FileReader();
          reader.readAsDataURL(res as Blob); 
          reader.onloadend = ()=>{
             this.imageUrl = this.sanitizer.bypassSecurityTrustUrl(reader.result as string);
          }
        },
        err=>console.log(err));
    })         
  }

   get Image(){
    this.GetPhoto();
    return this.imageUrl;
  }

}
