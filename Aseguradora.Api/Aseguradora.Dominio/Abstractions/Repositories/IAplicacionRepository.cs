using Aseguradora.Domain.Entities;

namespace Aseguradora.Domain.Abstractions.Repositories;

public interface IAplicacionRepository
{
    public Task<List<Aplicacion>> GetAllIngresadas();
    public Task<Aplicacion?> GetById(int id);
    public Task<List<Aplicacion>> FindByparameter(string parameter);
    public Task<int> Save(Aplicacion moneda);
}
