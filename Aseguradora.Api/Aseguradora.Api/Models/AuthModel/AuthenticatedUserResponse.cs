using Aseguradora.Api.Models.UsuarioModel;

namespace Aseguradora.Shared.Authentication;

public record AuthenticatedUserResponse(
    GetUserResponse usuario,
    string JwtToken,
    int IdMoneda
);
