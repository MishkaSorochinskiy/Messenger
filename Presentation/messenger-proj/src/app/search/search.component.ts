import { UserService, User } from './../services/user.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  SearchUsers:User[];
  filter:string=null;
  constructor(private userservice:UserService) { }

  ngOnInit() {
    this.userservice.searchdata.subscribe((res)=>this.SearchUsers=res);
  }

  search(){
    this.userservice.SearchUsers(this.filter);
  }

}
