import { AuthService } from './auth.service';
import { HttpInterceptor } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TokeninterceptorService implements HttpInterceptor{

constructor(private auth:AuthService) { }

intercept(req,next){
  let tokenizedreq=req.clone({
      withCredentials:true
  })
  return next.handle(tokenizedreq);
}

}
