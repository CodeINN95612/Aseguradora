import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EjecutivoAdminGuard } from './guards/ejecutivo-guard.guard';
import { HomeComponent } from './home/home.component';
import { ListaAplicacionComponent } from './lista-aplicacion/lista-aplicacion.component';
import { LoginComponent } from './login/login.component';
import { RevisionAplicacionComponent } from './revision-aplicacion/revision-aplicacion.component';
import { AplicacionComponent } from './aplicacion/aplicacion.component';
import { TrabajadorGuard } from './guards/trabajador-guard.guard';
import { ListaUsuariosComponent } from './usuarios/lista-usuarios/lista-usuarios.component';
import { AdminGuard } from './guards/admin-guard.guard';
import { UsuarioDetalle } from './models/Usuario';
import { DetalleUsuarioComponent } from './usuarios/detalle-usuario/detalle-usuario.component';
import { ListaEmpresasComponent } from './empresa/lista-empresas/lista-empresas.component';
import { DetalleEmpresaComponent } from './empresa/detalle-empresa/detalle-empresa.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'aplicaciones', component: ListaAplicacionComponent },
  { path: 'revisionAplicacion/:id', component: RevisionAplicacionComponent },
  { path: 'aplicacion', component: AplicacionComponent, canActivate: [TrabajadorGuard] },
  { path: 'usuarios', component: ListaUsuariosComponent, canActivate: [AdminGuard] },
  { path: 'usuario/:username', component: DetalleUsuarioComponent, canActivate: [AdminGuard] },
  { path: 'empresas', component: ListaEmpresasComponent, canActivate: [AdminGuard] },
  { path: 'empresa/:id', component: DetalleEmpresaComponent, canActivate: [AdminGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
