import { SafeUrl } from '@angular/platform-browser';
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

  public imageUrl:SafeUrl;
  constructor(private auth:AuthService,private photoser:PhotoService) { }

   ngOnInit() {
    this.photoser.GetPhoto();
  }

  async fileselected(event){
   (await this.photoser.UploadPhoto(event.target.files[0]))
        .subscribe((res=>this.photoser.GetPhoto()))
  }
}
