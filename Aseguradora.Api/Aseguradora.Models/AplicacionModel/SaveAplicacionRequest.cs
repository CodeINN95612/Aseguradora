namespace Aseguradora.Models.AplicacionModel;

public record SaveAplicacionRequest(
    DateTime Fecha,
    string Desde,
    string Hasta,
    string TipoTransporte,
    string Perteneciente,
    string Consignada,
    string EmbarcadoPor,
    DateTime? FechaEmbargue,
    DateTime? FechaLLegada,
    string NotaPredio,
    string OrdenCompra,
    SaveAplicacionAduanaRequest Aduana);
