using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Aseguradora.Auth.Data.Entities;
using Aseguradora.Domain.Abstractions.Common;
using Aseguradora.Domain.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Aseguradora.Auth.Services;

public sealed class JwtTokenGenerator : IJwtTokenGenerator
{
    private JwtSettings _jwtSettings;
    private IDateTimeProvider _datetimeProvider;

    public JwtTokenGenerator(IOptions<JwtSettings> jwtSettings, IDateTimeProvider datetimeProvider)
    {
        _jwtSettings = jwtSettings.Value;
        _datetimeProvider = datetimeProvider;
    }

    public string Generate(Usuario user)
    {
        var signingCredentials = new SigningCredentials(
        new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
        SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, user.UsuarioCampo),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var securityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: _datetimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
            claims: claims,
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);

    }
}