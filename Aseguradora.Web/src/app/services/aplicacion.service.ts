import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subscription } from 'rxjs';
import { URI } from 'src/env/Uri';
import { Aplicacion, AplicacionFull, AplicacionSave } from '../models/Aplicacion';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class AplicacionService {

  uri = URI;
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

    var rol = this.auth.getUserData()?.usuario?.rol ?? null

    if (rol?.esAdmin) {
      return this.http.get<Aplicacion[]>(this.uri + 'api/Aplicacion', httpOptions);
    }
    if (rol?.esEjecutivo) {
      return this.http.get<Aplicacion[]>(this.uri + 'api/Aplicacion/ingresadas', httpOptions);
    }

    console.log(this.auth.getUserData());

    var empresa = this.auth.getUserData()?.usuario?.empresa?.id;
    console.log(empresa);

    return this.http.get<Aplicacion[]>(this.uri + `api/Aplicacion/all?idEmpresa=${empresa}`, httpOptions);
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
    return this.http.get<AplicacionFull>(this.uri + `api/Aplicacion/getById?id=${id}`, httpOptions);
  }

  aprobar(id: number) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*',
        'Authorization': 'Bearer ' + this.auth.getUserData()?.jwtToken,
        'Access-Control-Allow-Headers': 'Origin, x-requested-with, authorization, content-type, unique-one-time-token, accept',
        'Access-Control-Allow-Methods': '*',
      })
    };
    return this.http.post<number>(this.uri + `api/Aplicacion/aprobar?IdAplicacion=${id}`, {}, httpOptions);
  }

  negar(id: number) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*',
        'Authorization': 'Bearer ' + this.auth.getUserData()?.jwtToken,
        'Access-Control-Allow-Headers': 'Origin, X-Requested-With, Content-Type, Accept',
        'Access-Control-Allow-Methods': '*'
      })
    };

    return this.http.post<number>(this.uri + `api/Aplicacion/negar?IdAplicacion=${id}`, {}, httpOptions);
  }

  ingresar(ap: AplicacionSave) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*',
        'Authorization': 'Bearer ' + this.auth.getUserData()?.jwtToken,
        'Access-Control-Allow-Headers': 'Origin, X-Requested-With, Content-Type, Accept',
        'Access-Control-Allow-Methods': '*'
      })
    };

    return this.http.post<number>(this.uri + `api/Aplicacion/ingresar`, ap, httpOptions);
  }
}
