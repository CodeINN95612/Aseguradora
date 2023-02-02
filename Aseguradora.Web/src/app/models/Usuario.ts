import { Empresa } from "./Empresa"
import { Rol } from "./Rol"

export type Usuario = {
    id: number,
    username: string,
    email: string,
    empresa?: Empresa,
    rol: Rol
}

export type UsuarioDetalle = {
    username: string,
    email: string,
    clave: string,
    idRol: number,
    idEmpresa: number | null
}