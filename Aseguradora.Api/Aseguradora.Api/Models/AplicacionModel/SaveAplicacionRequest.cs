namespace Aseguradora.Api.Models.AplicacionModel;

public record SaveAplicacionRequest(
    int Id,
    DateTime Fecha,
    string Desde,
    string Hasta,
    DateTime? FechaEmbargue,
    DateTime? FechaLLegada);
