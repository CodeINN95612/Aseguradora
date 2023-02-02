import { Component } from '@angular/core';
import { UsuarioService } from 'src/app/services/usuario.service';
import { Usuario, UsuarioDetalle } from '../../models/Usuario';
import { Rol } from '../../models/Rol';
import { ActivatedRoute, Router } from '@angular/router';
import { RolService } from '../../services/rol.service';
import { EmpresaService } from '../../services/empresa.service';
import { Empresa } from '../../models/Empresa';

@Component({
  selector: 'app-detalle-usuario',
  templateUrl: './detalle-usuario.component.html',
  styleUrls: ['./detalle-usuario.component.css']
})
export class DetalleUsuarioComponent {
  username: string = '';
  title: string = '';
  user: UsuarioDetalle;
  roles: Rol[] = [];
  empresas: Empresa[] = [];
  submitButtonText: string = '';

  constructor(private userService: UsuarioService, private route: ActivatedRoute,
    private rol: RolService, private empresa: EmpresaService, private router: Router) {
    this.user = {
      username: '',
      clave: '',
      email: '',
      idEmpresa: 1,
      idRol: 1,
    }
  }

  ngOnInit() {
    this.username = this.route.snapshot.paramMap.get('username') ?? '';
    this.title = 'Crear Usuario';
    this.submitButtonText = 'Crear';

    this.empresa.getAll().subscribe((empresas: any) => this.empresas = empresas);
    this.rol.getAll().subscribe((roles: any) => this.roles = roles);

    if (this.username == '')
      return;

    this.title = 'Editar Usuario';
    this.submitButtonText = 'Editar';
    this.userService.getByUsername(this.username).subscribe((u: any) => {
      this.user = {
        username: u.username,
        clave: '',
        email: u.email,
        idRol: u.rol.id,
        idEmpresa: u.empresa?.id ?? 0
      }
    });
  }

  onSubmit() {
    let usuario = { ...this.user };
    this.userService.save(usuario).subscribe(_ => this.router.navigate(['usuarios']));
  }
}
