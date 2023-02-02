import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { JwtModule } from '@auth0/angular-jwt';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { NavComponent } from './nav/nav.component';
import { AplicacionComponent } from './aplicacion/aplicacion.component';
import { TransporteComponent } from './transporte/transporte.component';
import { ListaAplicacionComponent } from './lista-aplicacion/lista-aplicacion.component';
import { RevisionAplicacionComponent } from './revision-aplicacion/revision-aplicacion.component';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ListaUsuariosComponent } from './usuarios/lista-usuarios/lista-usuarios.component';
import { DetalleUsuarioComponent } from './usuarios/detalle-usuario/detalle-usuario.component';
import { ListaEmpresasComponent } from './empresa/lista-empresas/lista-empresas.component';
import { DetalleEmpresaComponent } from './empresa/detalle-empresa/detalle-empresa.component'

export function tokenGetter() {
  return localStorage.getItem("jwt");
}

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    NavComponent,
    AplicacionComponent,
    TransporteComponent,
    ListaAplicacionComponent,
    RevisionAplicacionComponent,
    ListaUsuariosComponent,
    DetalleUsuarioComponent,
    ListaEmpresasComponent,
    DetalleEmpresaComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["localhost:7145"],
        disallowedRoutes: []
      }
    })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
