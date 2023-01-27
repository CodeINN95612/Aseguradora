import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthenticatedUser } from '../models/AutheticatedUser';
import { Router } from '@angular/router';



@Injectable({
  providedIn: 'root'
})
export class AuthService {

  uri = 'http://insertnickname-001-site1.btempurl.com'
  private userData!: AuthenticatedUser;

  constructor(private http: HttpClient, private router: Router) { }

  login(usuarioEmail: string, clave: string, idMoneda: number) {
    return this.http.post(this.uri + '/api/Authentication/login', { usuarioEmail, clave, idMoneda })
      .subscribe((res: any) => {
        this.userData = res;
        localStorage.setItem('userData', JSON.stringify(this.userData));
        if (this.userData.usuario.rol.esAdmin || this.userData.usuario.rol.esEjecutivo) {
          this.router.navigate(['/aplicaciones']);
        } else {
          this.router.navigate(['/aplicacion']);
        }
      });
  }

  getUserData(): AuthenticatedUser {
    return JSON.parse(localStorage.getItem('userData')!)
  }
}
