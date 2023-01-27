import { Usuario } from './Usuario';
import { Empresa } from './Empresa';
import { NumberSymbol } from '@angular/common';
export type Aplicacion = {
    id: number,
    numero: number,
    fecha: Date,
    usuario: Usuario,
    empresa: Empresa,
    desde: string,
    hasta: string,
    fechaEmbargue?: Date,
    fechaLLegada?: Date,
    estado: string,
};

export type Aduana = {
    idAplicacion: number;
    item: string;
    marca: string;
    numero: string;
    pesoBruto: number;
    bultos: number;
    montoTotal: number;
    porcentajeOtrosGastos: number;
    sumaAseguradora: number;
    valorPrima: number;
    descripcionContenido: string;
    observaciones: string;
}

export type AplicacionFull = {
    id: number;
    numero: number;
    fecha: Date;
    usuario: Usuario;
    empresa: Empresa;
    desde: string;
    hasta: string;
    tipoTransporte: string;
    perteneciente: string;
    consignada: string;
    embarcadoPor: string;
    fechaEmbarque?: Date;
    fechaLLegada?: Date;
    notaPredio: string;
    ordenCompra: string;
    aduana: Aduana;
    estado: string;
};

export type AduanaSave = {
    item: string,
    marca: string,
    numero: string,
    pesoBruto: number,
    bultos: number,
    montoTotal: number,
    porcentajeOtrosGastos: number,
    sumaAseguradora: number,
    valorPrima: number,
    descripcionContenido: string,
    observaciones: string
};

export type AplicacionSave = {
    fecha?: Date,
    desde: string,
    hasta: string,
    tipoTransporte: string,
    perteneciente: string,
    consignada: string,
    embarcadoPor: string,
    fechaEmbargue?: Date,
    fechaLLegada?: Date,
    notaPredio: string,
    ordenCompra: string,
    aduana: AduanaSave,
};