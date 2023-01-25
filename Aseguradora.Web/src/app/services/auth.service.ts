import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthenticatedUser } from '../models/AutheticatedUser';



@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private userData: AuthenticatedUser | undefined;

  constructor(private http: HttpClient) { }

  login(usuarioEmail: string, clave: string, idMoneda: number) {
    return this.http.post('http://insertnickname-001-site1.btempurl.com/api/Authentication/login', { usuarioEmail, clave, idMoneda })
      .subscribe((res: any) => {
        this.userData = res;
      });
  }

  getUserData(): AuthenticatedUser | undefined {
    return this.userData;
  }
}
