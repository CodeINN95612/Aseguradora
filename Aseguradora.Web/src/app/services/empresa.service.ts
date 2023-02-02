import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthService } from './auth.service';
import { Empresa } from '../models/Empresa';
import { URI } from 'src/env/Uri';

@Injectable({
  providedIn: 'root'
})
export class EmpresaService {

  uri = URI

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
    return this.http.get(this.uri + 'api/Empresa', httpOptions)
  }

  getById(id: number) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*',
        'Authorization': 'Bearer ' + this.auth.getUserData()?.jwtToken,
        'Access-Control-Allow-Headers': 'Origin, X-Requested-With, Content-Type, Accept'
      })
    };
    return this.http.get(this.uri + `api/Empresa/getById?id=${id}`, httpOptions)
  }

  save(empresa: Empresa) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*',
        'Authorization': 'Bearer ' + this.auth.getUserData()?.jwtToken,
        'Access-Control-Allow-Headers': 'Origin, X-Requested-With, Content-Type, Accept'
      })
    };
    return this.http.post(this.uri + `api/Empresa/save`, empresa, httpOptions)
  }
}
