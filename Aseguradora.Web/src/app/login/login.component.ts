import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http'
import { Router } from '@angular/router';
import { AuthResponse } from '../../interfaces/AuthResponse';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  form!: FormGroup;

  constructor(
    private _formBuilder: FormBuilder,
    private _http: HttpClient,
    private _router: Router) {

  }
  ngOnInit(): void {
    this.form = this._formBuilder.group({
      username: '',
      password: '',
      codigoMoneda: '',
    });
  }
  OnSubmit(): void {
    this._http.post<AuthResponse>('https://localhost:7145/api/User/login', this.form.getRawValue(), {
      withCredentials: true,
      headers: new HttpHeaders({ "Content-Type": "application/json" }),
    })
      .subscribe({
        next: (response: AuthResponse) => {
          const token = response.jwtToken;
          localStorage.setItem("jwt", token);
          this._router.navigate(["/"]);
        }
      });
  }
}
