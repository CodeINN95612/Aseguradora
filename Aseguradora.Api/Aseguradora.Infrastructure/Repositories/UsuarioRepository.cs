using Aseguradora.Auth.Data;
using Aseguradora.Auth.Data.Entities;
using Aseguradora.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aseguradora.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private AseguradoraDBContext _db;

    public UsuarioRepository(AseguradoraDBContext db)
    {
        _db = db;
    }

    public Task<List<Usuario>> GetAll()
    {
        return _db.ListaUsuarios.ToListAsync();
    }

    public Task<Usuario?> GetByUsername(string username)
    {
        return _db.ListaUsuarios.FirstOrDefaultAsync(u => u.UsuarioCampo == username);
    }

    public async Task<int> Save(Usuario usuario)
    {
        var state = usuario.Id == 0 ? EntityState.Added : EntityState.Modified;
        _db.Entry(usuario).State = state;
        await _db.SaveChangesAsync();
        return usuario.Id;
    }
}
