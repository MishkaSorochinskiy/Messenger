import { PhotoService } from './../services/photo.service';
import { AuthService } from './../services/auth.service';
import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';


@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  constructor(private auth:AuthService,private photoser:PhotoService) { }

  ngOnInit() {
    this.photoser.GetPhoto();
  }

  fileselected(event){
    this.photoser.UploadPhoto(event.target.files[0]);
  }

}
