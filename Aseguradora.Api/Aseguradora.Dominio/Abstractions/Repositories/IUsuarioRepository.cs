using Aseguradora.Domain.Entities;

namespace Aseguradora.Domain.Abstractions.Repositories;

public interface IUsuarioRepository
{
    public Task<List<Usuario>> GetAll();
    public Task<Usuario?> GetById(int Id);
    public Task<Usuario?> GetByUsername(string username);
    public Task<Usuario?> GetByUnique(string parameter);
    public Task<string> Save(Usuario usuario);
}
