namespace Aseguradora.Shared.Authentication;

public record AuthenticatedUser(
    string Usuario,
    string CodigoRol,
    string CodigoMoneda
);
