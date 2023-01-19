namespace Aseguradora.Api.Models.AuthModel;

public record LoginRequest(
    string Usuario,
    string Clave,
    int IdMoneda
);