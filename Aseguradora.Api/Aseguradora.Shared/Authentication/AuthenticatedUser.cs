namespace Aseguradora.Shared.Authentication;

public record AuthenticatedUser(
    string Usuario,
    string JwtToken,
    string CodigoRol,
    string CodigoMoneda
);
