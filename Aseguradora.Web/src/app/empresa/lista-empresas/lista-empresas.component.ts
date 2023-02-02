import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { EmpresaService } from '../../services/empresa.service';
import { Empresa } from '../../models/Empresa';

@Component({
  selector: 'app-lista-empresas',
  templateUrl: './lista-empresas.component.html',
  styleUrls: ['./lista-empresas.component.css']
})
export class ListaEmpresasComponent implements OnInit {
  empresas: Empresa[] = []
  /**
 *
 */
  constructor(private empresaService: EmpresaService, private router: Router) {

  }

  ngOnInit(): void {
    this.empresaService.getAll().subscribe((empresas: any) => this.empresas = empresas);
  }

  goToDetalle(id: number) {
    this.router.navigate(['empresa', id]);
  }
}
