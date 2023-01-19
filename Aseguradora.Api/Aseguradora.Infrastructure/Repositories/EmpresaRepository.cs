using Aseguradora.Auth.Data;
using Aseguradora.Domain.Abstractions.Repositories;
using Aseguradora.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aseguradora.Infrastructure.Repositories;

public class EmpresaRepository : IEmpresaRepository
{
    private AseguradoraDBContext _db;

    public EmpresaRepository(AseguradoraDBContext db)
    {
        _db = db;
    }

    public Task<List<Empresa>> GetAll()
    {
        return _db.ListaEmpresas.ToListAsync();
    }

    public Task<Empresa?> GetById(int id)
    {
        return _db.ListaEmpresas.FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<int> Save(Empresa Empresa)
    {
        var state = Empresa.Id == 0 ? EntityState.Added : EntityState.Modified;
        _db.Entry(Empresa).State = state;
        await _db.SaveChangesAsync();
        return Empresa.Id;
    }
}
