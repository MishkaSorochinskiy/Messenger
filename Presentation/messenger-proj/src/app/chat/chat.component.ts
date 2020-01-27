import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {
  weather;
  constructor(private http:HttpClient) { }

  ngOnInit() {
  }

  refresh(){
    this.http.get('https://localhost:44334/api/weatherforecast')
    .subscribe(data=>this.weather=(data));
  }

}
