namespace Aseguradora.Api.Models.AuthModel;

public record LoginRequest(
    string UsuarioEmail,
    string Clave,
    int IdMoneda
);