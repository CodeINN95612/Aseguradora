import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UsuarioService } from 'src/app/services/usuario.service';
import { Usuario } from '../../models/Usuario';

@Component({
  selector: 'app-lista-usuarios',
  templateUrl: './lista-usuarios.component.html',
  styleUrls: ['./lista-usuarios.component.css']
})
export class ListaUsuariosComponent {
  usuarios!: Usuario[];

  constructor(private usuarioService: UsuarioService, private router: Router) { }

  ngOnInit() {
    this.usuarioService.getAll().subscribe((usuarios: any) => {
      this.usuarios = usuarios;
    });
  }

  goToDetalle(username: string) {
    this.router.navigate(['/usuario', username]);
  }
}
