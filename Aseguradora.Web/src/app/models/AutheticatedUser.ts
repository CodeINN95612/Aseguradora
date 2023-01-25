import { Usuario } from './Usuario';
import { Moneda } from './Moneda';

export type AuthenticatedUser = {
    moneda: Moneda,
    jwtToken: string,
    usuario: Usuario
}