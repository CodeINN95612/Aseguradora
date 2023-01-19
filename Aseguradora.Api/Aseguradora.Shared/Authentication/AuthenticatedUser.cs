namespace Aseguradora.Shared.Authentication;

public record AuthenticatedUser(
    string Usuario,
    string JwtToken,
    int IdRol,
    int IdMoneda
);
