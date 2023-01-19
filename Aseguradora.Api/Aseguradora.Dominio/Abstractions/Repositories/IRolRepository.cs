using Aseguradora.Domain.Entities;

namespace Aseguradora.Domain.Abstractions.Repositories;

public interface IRolRepository
{
    public Task<List<Rol>> GetAll();
    public Task<Rol?> GetById(int id);
    public Task<int> Save(Rol moneda);
}
