using Aseguradora.Auth.Data.Entities;

namespace Aseguradora.Domain.Abstractions.Common;

public interface IJwtTokenGenerator
{
    public string Generate(Usuario user);
}