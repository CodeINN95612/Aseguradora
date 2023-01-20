using Aseguradora.Auth.Data;
using Aseguradora.Domain.Abstractions.Repositories;
using Aseguradora.Domain.Entities;
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
        return _db.ListaUsuarios
            .Include(u => u.Rol)
            .Include(u => u.Empresa)
            .ToListAsync();
    }

    public Task<Usuario?> GetById(int Id)
    {
        return _db.ListaUsuarios
            .Include(u => u.Rol)
            .Include(u => u.Empresa)
            .FirstOrDefaultAsync(u => u.Id == Id);
    }

    public Task<Usuario?> GetByUnique(string parameter)
    {
        return _db.ListaUsuarios
            .Include(u => u.Rol)
            .Include(u => u.Empresa)
            .FirstOrDefaultAsync(u => u.Email == parameter || u.UsuarioCampo == parameter);
    }

    public Task<Usuario?> GetByUsername(string username)
    {
        return _db.ListaUsuarios
            .Include(u => u.Rol)
            .Include(u => u.Empresa)
            .FirstOrDefaultAsync(u => u.UsuarioCampo == username);
    }

    public async Task<string> Save(Usuario usuario)
    {
        var state = usuario.Id == 0 ? EntityState.Added : EntityState.Modified;
        _db.Entry(usuario).State = state;
        await _db.SaveChangesAsync();
        return usuario.UsuarioCampo;
    }
}
