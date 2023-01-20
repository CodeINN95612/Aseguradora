using Aseguradora.Domain.Entities;

namespace Aseguradora.Domain.Abstractions.Common;

public interface IJwtTokenGenerator
{
    public string Generate(Usuario user);
    public Dictionary<string, string> GetClaims(string token);
}