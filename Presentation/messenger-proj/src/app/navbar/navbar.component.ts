import { AuthService } from './../services/auth.service';
import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';


@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  islogged:boolean;
  constructor(private auth:AuthService) { }

  ngOnInit() {
    this.islogged=this.auth.looggedIn();
  }

  signout(){
    this.auth.signout();
    this.islogged=false;
  }
}
