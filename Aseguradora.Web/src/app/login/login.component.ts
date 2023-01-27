import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { MonedaService } from '../services/moneda.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  monedas: any[] = [];
  moneda: number;
  username: string;
  password: string;
  isLoading = true;

  constructor(private http: HttpClient, private authService: AuthService, private monedaService: MonedaService) {
    this.moneda = 1;
    this.username = '';
    this.password = '';
  }

  ngOnInit() {
    this.monedaService.getMonedas().subscribe((data: any[]) => {
      this.monedas = data;
      this.isLoading = false;
    });
  }

  onSubmit() {
    this.authService.login(this.username, this.password, this.moneda);
  }

}
