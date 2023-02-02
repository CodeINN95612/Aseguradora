// moneda.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Moneda } from '../models/Moneda';
import { URI } from 'src/env/Uri';

@Injectable({
  providedIn: 'root'
})
export class MonedaService {

  uri = URI
  constructor(private http: HttpClient) { }

  getMonedas(): Observable<Moneda[]> {
    return this.http.get<Moneda[]>(this.uri + 'api/Moneda');
  }
}

