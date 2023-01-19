using Aseguradora.Auth.Data;
using Aseguradora.Domain.Abstractions.Repositories;
using Aseguradora.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aseguradora.Infrastructure.Repositories;

public class AplicacionRepository : IAplicacionRepository
{
    private AseguradoraDBContext _db;

    public AplicacionRepository(AseguradoraDBContext db)
    {
        _db = db;
    }

    public Task<List<Aplicacion>> GetAll()
    {
        return _db.ListaAplicaciones.ToListAsync();
    }

    public Task<Aplicacion?> GetById(int id)
    {
        return _db.ListaAplicaciones.FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<int> Save(Aplicacion Aplicacion)
    {
        var state = Aplicacion.Id == 0 ? EntityState.Added : EntityState.Modified;
        _db.Entry(Aplicacion).State = state;
        await _db.SaveChangesAsync();
        return Aplicacion.Id;
    }
}
