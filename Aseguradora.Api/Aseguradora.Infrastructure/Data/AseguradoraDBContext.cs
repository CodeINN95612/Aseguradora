using Aseguradora.Auth.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aseguradora.Auth.Data;

public class AseguradoraDBContext : DbContext
{
	public AseguradoraDBContext(DbContextOptions<AseguradoraDBContext> options) 
		: base(options)
	{

	}

	public virtual DbSet<Usuario> ListaUsuarios { get; set; } = null!;
	public virtual DbSet<Moneda> ListaMonedas { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Usuario>().HasData(
			new Usuario
			{
				Id = 1,
				UsuarioCampo = "admin",
				Clave = "admin"
			}
		);

		modelBuilder.Entity<Moneda>().HasData(
			new Moneda 
			{ 
				Id = 1,
				Codigo = "USD",
				Nombre = "Dolar Estadounidense"
			},
			new Moneda
			{
				Id = 2,
				Codigo = "EU",
				Nombre = "Euro"
			}
		);
    }
}
//Hola
