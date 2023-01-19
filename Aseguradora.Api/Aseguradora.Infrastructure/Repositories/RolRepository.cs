using Aseguradora.Auth.Data;
using Aseguradora.Domain.Abstractions.Repositories;
using Aseguradora.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aseguradora.Infrastructure.Repositories;

public class RolRepository : IRolRepository
{
    private AseguradoraDBContext _db;

    public RolRepository(AseguradoraDBContext db)
    {
        _db = db;
    }

    public Task<List<Rol>> GetAll()
    {
        return _db.ListaRoles.ToListAsync();
    }

    public Task<Rol?> GetById(int id)
    {
        return _db.ListaRoles.FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<int> Save(Rol Rol)
    {
        var state = Rol.Id == 0 ? EntityState.Added : EntityState.Modified;
        _db.Entry(Rol).State = state;
        await _db.SaveChangesAsync();
        return Rol.Id;
    }
}
