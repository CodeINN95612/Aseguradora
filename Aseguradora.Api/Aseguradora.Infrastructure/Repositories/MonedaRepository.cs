using Aseguradora.Auth.Data;
using Aseguradora.Domain.Abstractions.Repositories;
using Aseguradora.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aseguradora.Infrastructure.Repositories;

public class MonedaRepository : IMonedaRepository
{
    private AseguradoraDBContext _db;

    public MonedaRepository(AseguradoraDBContext db)
    {
        this._db = db;
    }

    public Task<List<Moneda>> GetAll()
    {
        return _db.ListaMonedas.ToListAsync();
    }

    public Task<Moneda?> GetById(int id)
    {
        return _db.ListaMonedas.FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<int> Save(Moneda moneda)
    {
        var state = moneda.Id == 0 ? EntityState.Added : EntityState.Modified;
        _db.Entry(moneda).State = state;
        await _db.SaveChangesAsync();
        return moneda.Id;
    }
}
