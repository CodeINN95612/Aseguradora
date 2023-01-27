// moneda.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Moneda } from '../models/Moneda';

@Injectable({
  providedIn: 'root'
})
export class MonedaService {

  uri = 'http://insertnickname-001-site1.btempurl.com'
  constructor(private http: HttpClient) { }

  getMonedas(): Observable<Moneda[]> {
    return this.http.get<Moneda[]>(this.uri + '/api/Moneda');
  }
}

