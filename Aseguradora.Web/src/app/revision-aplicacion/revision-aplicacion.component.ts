import { Component, OnInit } from '@angular/core';
import { AplicacionService } from 'src/app/services/aplicacion.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AplicacionFull } from '../models/Aplicacion';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-revision-aplicacion',
  templateUrl: './revision-aplicacion.component.html',
  styleUrls: ['./revision-aplicacion.component.css']
})
export class RevisionAplicacionComponent implements OnInit {

  polizaFull!: AplicacionFull;
  isLoading: boolean = true;
  muestraBotones = false;

  constructor(private aplicacionService: AplicacionService,
    private route: ActivatedRoute, private router: Router, private auth: AuthService) { }

  ngOnInit(): void {
    const id = +(this.route.snapshot.paramMap.get('id') ?? 0);
    this.aplicacionService.getById(id).subscribe(poliza => {
      this.polizaFull = poliza;
      this.isLoading = true;
    }
    );

    let rol = this.auth.getUserData()?.usuario.rol;

    if (rol?.esEjecutivo || rol?.esAdmin) {
      this.muestraBotones = true;
    }
  }

  aprobarPoliza(): void {
    this.isLoading = true;
    const id = +(this.route.snapshot.paramMap.get('id') ?? 0);
    this.aplicacionService.aprobar(id).subscribe(id => {
      this.isLoading = false;
      this.router.navigate(['aplicaciones'])
    })
  }

  negarPoliza(): void {
    this.isLoading = true;
    const id = +(this.route.snapshot.paramMap.get('id') ?? 0);
    this.aplicacionService.negar(id).subscribe(id => {
      this.isLoading = false;
      this, this.router.navigate(['aplicaciones']);
    })
  }
}
