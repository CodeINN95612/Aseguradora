using Aseguradora.Auth.Data.Entities;

namespace Aseguradora.Domain.Abstractions.Repositories;

public interface IUsuarioRepository
{
    public Task<Usuario?> GetByUsername(string username);
    public Task<int> SaveUser(Usuario usuario);
}
