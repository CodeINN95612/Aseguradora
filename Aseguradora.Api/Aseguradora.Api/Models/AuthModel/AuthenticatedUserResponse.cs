using Aseguradora.Api.Models.MonedaModel;
using Aseguradora.Api.Models.UsuarioModel;

namespace Aseguradora.Shared.Authentication;

public record AuthenticatedUserResponse(
    GetUserResponse usuario,
    string JwtToken,
    GetMonedaResponse moneda
);
