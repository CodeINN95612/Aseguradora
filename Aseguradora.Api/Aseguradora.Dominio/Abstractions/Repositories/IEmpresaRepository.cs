using Aseguradora.Domain.Entities;

namespace Aseguradora.Domain.Abstractions.Repositories;

public interface IEmpresaRepository
{
    public Task<List<Empresa>> GetAll();
    public Task<Empresa?> GetById(int id);
    public Task<int> Save(Empresa Empresa);
}
