import { ConfigService } from './config.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { ChildActivationStart } from '@angular/router';
import { getUrlScheme } from '@angular/compiler';

@Injectable({
  providedIn: 'root'
})
export class PhotoService {

  imageUrl;
  usersUrl:{id:number,url:SafeUrl}[]=[];

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

  async GetPhotoById(id:number){
    let url = await this.config.getConfig("getphotobyid");
    url=`${url}?id=${id}`;
   
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Accept': 'application/json'
    });
    return this.http.get(url,{responseType:"blob" as "json",headers:headers});
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

  async GetUrl(id:number){
    console.log(this.usersUrl);
    console.log(this.usersUrl.length);
    if(this.usersUrl.find(u=>u.id==id)==undefined){
       (await this.GetPhotoById(id)).toPromise()
          .then(
            async res=>{
              let userurl= await this.ConvertPhotoAsync(res as Blob);
              this.usersUrl.push({id:id,url:userurl});
              return new Promise((res,rej)=>res(this.usersUrl.find(u=>u.id==id).url));
          });
    }
    else{
      console.log(this.usersUrl);
      return new Promise((res,rej)=>res(this.usersUrl.find(u=>u.id==id).url));
    }
  }
}
