import { UserService, User } from './../services/user.service';
import { Component, OnInit } from '@angular/core';
import { ChatService } from '../services/chat.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  SearchUsers:User[];
  filter:string=null;
  constructor(private userservice:UserService,private chatservice:ChatService) { }

  ngOnInit() {
    this.userservice.searchdata.subscribe((res)=>this.SearchUsers=res);
  }

  search(){
    this.userservice.SearchUsers(this.filter);
  }

  createChat(userid:number){
    this.chatservice.CreateChate(userid);
    this.userservice.updateSearchUsers([]);
  }

}
