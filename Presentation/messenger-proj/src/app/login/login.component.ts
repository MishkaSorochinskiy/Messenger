import { AuthService } from './../services/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public isnotvalid=false;
  userdata={email:'',password:''};
  constructor(private auth:AuthService) { }

  ngOnInit() {
  }

  async signin(){
    await this.auth.signin(this.userdata)
    .then(()=>{
      this.isnotvalid=true;
    });
  }

}
