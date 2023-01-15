using Aseguradora.Auth.Data;
using Aseguradora.Auth.Data.Entities;
using Aseguradora.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aseguradora.Infrastructure.Repositories;

public class MonedaRepository : IMonedaRepository
{
    private AseguradoraDBContext _db;

    public Task<List<Moneda>> GetAll()
    {
        return _db.ListaMonedas.ToListAsync();
    }

    public Task<Moneda?> GetById(int id)
    {
        return _db.ListaMonedas.FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<int> SaveMoneda(Moneda moneda)
    {
        var state = moneda.Id == 0 ? EntityState.Added : EntityState.Modified;
        _db.Entry(moneda).State = state;
        await _db.SaveChangesAsync();
        return moneda.Id;
    }
}
