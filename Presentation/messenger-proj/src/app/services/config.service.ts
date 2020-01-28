import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class ConfigService {

  configUrl = 'assets/config.json';

  constructor(private http:HttpClient) { }

  async getConfig() {
    return await this.http.get(this.configUrl)
  }
  
}