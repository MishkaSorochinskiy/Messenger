import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { resolve } from 'url';


@Injectable({
  providedIn: 'root'
})
export class ConfigService {

  configUrl = 'assets/config.json';

  constructor(private http:HttpClient) { }

  async getConfig(key:string) {
     let data=await this.http.get(this.configUrl).toPromise()
     return new Promise<string>((res,rej)=>res(data[key]));
  }
  
}