import { Component } from '@angular/core';
import { AplicacionSave } from '../models/Aplicacion';
import { AplicacionService } from '../services/aplicacion.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-aplicacion',
  templateUrl: './aplicacion.component.html',
  styleUrls: ['./aplicacion.component.css']
})
export class AplicacionComponent {
  aplicacionSave: AplicacionSave;

  constructor(private aplicacionService: AplicacionService,
    private router: Router, private toastr: ToastrService) {
    this.aplicacionSave = {
      desde: '',
      hasta: '',
      consignada: '',
      embarcadoPor: '',
      notaPredio: '',
      ordenCompra: '',
      perteneciente: '',
      tipoTransporte: '',
      fecha: new Date(),
      fechaEmbargue: undefined,
      fechaLLegada: undefined,
      aduana: {
        bultos: 0,
        descripcionContenido: '',
        item: '',
        marca: '',
        montoTotal: 0.00,
        numero: '',
        observaciones: '',
        pesoBruto: 0.00,
        porcentajeOtrosGastos: 0.00,
        sumaAseguradora: 0.00,
        valorPrima: 0.00,

      }
    }
  }

  onSubmit() {
    this.aplicacionService.ingresar(this.aplicacionSave).
      subscribe(_ => {
        this.toastr.show("Guardado!");
        this.router.navigateByUrl('/', { skipLocationChange: true }).then(() =>
          this.router.navigate([this.router.url]));
      });
  }
}
