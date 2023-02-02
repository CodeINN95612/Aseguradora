import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthService } from './auth.service';
import { UsuarioDetalle } from '../models/Usuario';
import { URI } from 'src/env/Uri';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {
  uri = URI
  // uri = 'https://localhost:7150/'
  constructor(private http: HttpClient, private auth: AuthService) { }

  getAll() {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*',
        'Authorization': 'Bearer ' + this.auth.getUserData()?.jwtToken,
        'Access-Control-Allow-Headers': 'Origin, X-Requested-With, Content-Type, Accept'
      })
    };
    return this.http.get(this.uri + 'api/Usuario', httpOptions)
  }

  getByUsername(username: string) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*',
        'Authorization': 'Bearer ' + this.auth.getUserData()?.jwtToken,
        'Access-Control-Allow-Headers': 'Origin, X-Requested-With, Content-Type, Accept'
      })
    };
    return this.http.get(this.uri + `api/Usuario/getByUsername?username=${username}`, httpOptions)
  }

  save(usuario: UsuarioDetalle) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*',
        'Authorization': 'Bearer ' + this.auth.getUserData()?.jwtToken,
        'Access-Control-Allow-Headers': 'Origin, X-Requested-With, Content-Type, Accept',
        'Access-Control-Allow-Methods': '*'
      }),
      responseType: 'text' as const
    };
    return this.http.post(this.uri + `api/Usuario/save`, usuario, httpOptions);
  }
}
