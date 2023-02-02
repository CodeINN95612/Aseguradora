import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class EjecutivoAdminGuard implements CanActivate {
  constructor(private authService: AuthService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const rol = this.authService.getUserData()?.usuario?.rol;

    if (!rol) {
      return false;
    }
    return rol.esEjecutivo || rol.esAdmin;
  }
}

