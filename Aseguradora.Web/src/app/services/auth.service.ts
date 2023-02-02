import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthenticatedUser } from '../models/AutheticatedUser';
import { Router } from '@angular/router';
import { URI } from 'src/env/Uri';



@Injectable({
  providedIn: 'root'
})
export class AuthService {

  uri = URI
  private userData!: AuthenticatedUser;

  constructor(private http: HttpClient, private router: Router) { }

  login(usuarioEmail: string, clave: string, idMoneda: number) {
    return this.http.post(this.uri + 'api/Authentication/login', { usuarioEmail, clave, idMoneda })
      .subscribe((res: any) => {
        this.userData = res;
        localStorage.setItem('userData', JSON.stringify(this.userData));
        window.location.reload();
      });
  }

  getUserData(): AuthenticatedUser | undefined {
    let userdata = localStorage.getItem('userData');
    if (!userdata)
      return undefined
    return JSON.parse(userdata);
  }
}
