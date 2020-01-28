import { AuthService } from './../services/auth.service';
import { Component, OnInit } from '@angular/core';
import { NgZone  } from '@angular/core';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  userdata={email:'',password:''};
  constructor(private auth:AuthService,private zone:NgZone) { }

  ngOnInit() {
  }

  signin(){
    this.auth.signin(this.userdata);
  }

}
