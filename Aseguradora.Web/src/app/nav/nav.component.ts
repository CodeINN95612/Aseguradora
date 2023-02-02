import { Component, OnInit } from '@angular/core';
import { Rol } from '../models/Rol';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  role: Rol | undefined;

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    let userData = this.authService.getUserData();

    this.role = userData?.usuario?.rol ?? undefined;
  }
}

