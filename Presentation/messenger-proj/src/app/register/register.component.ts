import { AuthService } from './../services/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {


  userdata={email:'',password:'',passwordconfirm:''};
  constructor(private auth:AuthService) { }

  ngOnInit() {
  }

  register(){
    this.auth.register(this.userdata);
  }

}
