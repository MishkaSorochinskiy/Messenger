import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RegisterGuard implements CanActivate {

  isenabled:boolean=false;

  constructor(private route:Router){

  }

  canActivate():boolean{
    if(this.isenabled){
      return this.isenabled;
    }
    else{
      this.route.navigate(['/signin']);
    }
  }
  
}
