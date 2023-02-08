using Aseguradora.Models.MonedaModel;
using Aseguradora.Models.UsuarioModel;

namespace Aseguradora.Models.AuthModel;

public record AuthenticatedUserResponse(
    GetUserResponse usuario,
    string JwtToken,
    GetMonedaResponse moneda
);
