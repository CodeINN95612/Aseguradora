using Aseguradora.Domain.Abstractions.Common;
using Aseguradora.Infrastructure.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Aseguradora.Api.Controllers;

[ApiController]
[Authorize]
public class AseguradoraController : ControllerBase
{
    public AseguradoraController()
    {
        //if (!Request.Headers.ContainsKey("Authorization"))
        //    return;

        //var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        //var claims = _jwt.GetClaims(token);

        //if (!claims.ContainsKey(JwtRegisteredClaimNames.Sub))
        //{
        //    throw new InvalidOperationException();
        //}

        //CurrentUser = claims[JwtRegisteredClaimNames.Sub];
    }
}
