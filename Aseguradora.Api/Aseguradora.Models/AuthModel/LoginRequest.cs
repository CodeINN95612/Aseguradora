namespace Aseguradora.Models.AuthModel;

public record LoginRequest(
    string UsuarioEmail,
    string Clave,
    int IdMoneda
);