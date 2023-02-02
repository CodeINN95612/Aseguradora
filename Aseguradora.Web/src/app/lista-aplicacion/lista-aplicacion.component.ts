import { Component } from '@angular/core';
import { Aplicacion } from '../models/Aplicacion';
import { AplicacionService } from '../services/aplicacion.service';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-lista-aplicacion',
  templateUrl: './lista-aplicacion.component.html',
  styleUrls: ['./lista-aplicacion.component.css']
})
export class ListaAplicacionComponent {
  polizas: Aplicacion[] = [];
  polizasFiltradas: Aplicacion[] = [];
  isLoading: boolean = true;
  searchTerm: string = '';

  constructor(private aplicacionService: AplicacionService, private router: Router, private auth: AuthService) { }

  ngOnInit(): void {
    this.getPolizas();

  }

  getPolizas(): void {
    this.aplicacionService.getAll().subscribe(polizas => {
      this.polizas = polizas;
      this.polizasFiltradas = polizas;
      this.isLoading = false;
    }
    );
  }

  filterPolizas() {
    if (this.searchTerm === '') {
      this.polizasFiltradas = this.polizas;
      return;
    }
    this.polizasFiltradas = this.polizas.filter(poliza => {
      return poliza.empresa.nombreEmpresa.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
        poliza.usuario.username.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
        poliza.numero.toString().includes(this.searchTerm);
    });
  }

  verDetalles(poliza: Aplicacion) {
    this.router.navigate(['/revisionAplicacion', poliza.id]);
  }
}
