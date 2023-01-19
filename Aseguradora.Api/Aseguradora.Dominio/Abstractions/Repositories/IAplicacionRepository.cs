using Aseguradora.Domain.Entities;

namespace Aseguradora.Domain.Abstractions.Repositories;

public interface IAplicacionRepository
{
    public Task<List<Aplicacion>> GetAll();
    public Task<Aplicacion?> GetById(int id);
    public Task<int> Save(Aplicacion moneda);
}
