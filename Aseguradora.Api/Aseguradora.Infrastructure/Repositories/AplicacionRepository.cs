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

    public async Task<List<Aplicacion>> FindByparameter(string parameter)
    {
        parameter = parameter.Trim().ToLower();
        List<Aplicacion> aplicacions = await _db.ListaAplicaciones
            .Include(a => a.Empresa)
            .Include(a => a.Usuario)
            .Where(a => a.Numero.ToString().ToLower().Contains(parameter) ||
                        a.Usuario.UsuarioCampo.ToLower().Contains(parameter) ||
                        a.Empresa.RUC.ToLower().Contains(parameter) ||
                        a.Empresa.Name.ToLower().Contains(parameter) ||
                        a.Empresa.Email.ToLower().Contains(parameter)).ToListAsync();
        return aplicacions;
    }

    public Task<List<Aplicacion>> GetAllIngresadas()
    {
        return _db.ListaAplicaciones
            .Where(a => a.Estado == null)
            .Include(a => a.Empresa)
            .Include(a => a.Usuario)
            .Include(a => a.Aduana)
            .ToListAsync();
    }

    public Task<Aplicacion?> GetById(int id)
    {
        return _db.ListaAplicaciones
            .Include(a => a.Empresa)
            .Include(a => a.Usuario)
            .Include(a => a.Aduana)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<int> Save(Aplicacion aplicacion)
    {
        var state = aplicacion.Id == 0 ? EntityState.Added : EntityState.Modified;
        _db.Entry(aplicacion).State = state;

        if(state ==EntityState.Added)
        {
            aplicacion.Aduana.Aplicacion = aplicacion;
            _db.Entry(aplicacion.Aduana).State = state;
        }

        await _db.SaveChangesAsync();
        return aplicacion.Id;
    }
}
