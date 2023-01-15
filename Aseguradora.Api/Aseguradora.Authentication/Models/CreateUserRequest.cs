namespace Aseguradora.Authentication.Models;

public record CreateUserRequest(
    string Usuario,
    string Clave
);