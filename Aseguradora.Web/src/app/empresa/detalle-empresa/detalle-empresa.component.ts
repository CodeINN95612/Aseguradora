import { Component } from '@angular/core';
import { Empresa } from 'src/app/models/Empresa';
import { EmpresaService } from 'src/app/services/empresa.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-detalle-empresa',
  templateUrl: './detalle-empresa.component.html',
  styleUrls: ['./detalle-empresa.component.css']
})
export class DetalleEmpresaComponent {
  id: number = 0;
  title: string = '';
  empresa!: Empresa;
  submitButtonText: string = '';

  constructor(private empresaService: EmpresaService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    this.id = +(this.route.snapshot.paramMap.get('id') ?? 0)
    this.title = 'Detalle Empresa';
    this.submitButtonText = 'Guardar';

    this.empresa = {
      id: 0,
      nombreEmpresa: '',
      email: '',
      ruc: ''
    };

    if (this.id == 0) {
      return;
    }

    this.empresaService.getById(this.id).subscribe((e: any) => this.empresa = e);
  }

  onSubmit() {
    this.empresaService.save(this.empresa).subscribe(result => {
      this.router.navigate(['empresas']);
    });
  }
}
