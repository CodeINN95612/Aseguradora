namespace Aseguradora.Models.AplicacionModel;

public record GetAplicacionAduanaResponse(
    int IdAplicacion,
    string Item,
    string Marca,
    string Numero,
    decimal PesoBruto,
    int Bultos,
    decimal MontoTotal,
    decimal PorcentajeOtrosGastos,
    decimal SumaAseguradora,
    decimal ValorPrima,
    string DescripcionContenido,
    string Observaciones);
