using Aseguradora.Domain.Entities;

namespace Aseguradora.Domain.Abstractions.Repositories;

public interface IMonedaRepository
{
    public Task<List<Moneda>> GetAll();
    public Task<Moneda?> GetById(int id);
    public Task<int> Save(Moneda moneda);
}
