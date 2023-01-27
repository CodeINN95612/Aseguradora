import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EjecutivoAdminGuard } from './guards/ejecutivo-guard.guard';
import { HomeComponent } from './home/home.component';
import { ListaAplicacionComponent } from './lista-aplicacion/lista-aplicacion.component';
import { LoginComponent } from './login/login.component';
import { RevisionAplicacionComponent } from './revision-aplicacion/revision-aplicacion.component';
import { AplicacionComponent } from './aplicacion/aplicacion.component';
import { TrabajadorGuard } from './guards/trabajador-guard.guard';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'aplicaciones', component: ListaAplicacionComponent, canActivate: [EjecutivoAdminGuard] },
  { path: 'revisionAplicacion/:id', component: RevisionAplicacionComponent },
  { path: 'aplicacion', component: AplicacionComponent, canActivate: [TrabajadorGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
