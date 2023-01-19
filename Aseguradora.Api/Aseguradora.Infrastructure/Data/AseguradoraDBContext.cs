using Aseguradora.Auth.Data.Entities;
using Aseguradora.Domain.Entities;
using Aseguradora.Infrastructure.Data.Configurations;
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
	public virtual DbSet<Aplicacion> ListaAplicaciones { get; set; } = null!;
	public virtual DbSet<Empresa> ListaEmpresas { get; set; } = null!;
	public virtual DbSet<Rol> ListaRoles { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

		modelBuilder.ApplyConfiguration(new AplicacionConfiguration());

		modelBuilder.Entity<Rol>().HasData(new Rol
		{
			Id = 1,
			Nombre = "Administrador Global",
			EsAdministrador = true,
			EsEjecutivo = false,
			EsTrabajador = false,
		});

        modelBuilder.Entity<Rol>().HasData(new Rol
        {
            Id = 2,
            Nombre = "Trabajador",
            EsAdministrador = false,
            EsEjecutivo = false,
            EsTrabajador = true,
        });

        modelBuilder.Entity<Usuario>().HasData(
			new Usuario
			{
				Id = 1,
				UsuarioCampo = "admin",
				Clave = "admin",
				IdRol = 1
			}
		) ;

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
