import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-channel-create',
  templateUrl: './channel-create.component.html',
  styleUrls: ['./channel-create.component.css']
})
export class ChannelCreateComponent implements OnInit {

  values:string[]=["1","2","3"];
  constructor() { }

  ngOnInit() {
  }

}
