using Aseguradora.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aseguradora.Infrastructure.Data.Configurations;

public class AplicacionConfiguration : IEntityTypeConfiguration<Aplicacion>
{
    public void Configure(EntityTypeBuilder<Aplicacion> builder)
    {
        builder.Property(a => a.Numero)
            .HasComputedColumnSql("[Id]");
        //builder.HasOne(a => a.EmpresaAsegurado)
        //    .WithMany()
        //    .HasForeignKey(a => a.IdEmpresaAsegurado);

        //builder.HasOne(l => l.EmpresaPagador)
        //        .WithMany()
        //        .HasForeignKey(l => l.IdEmpresaPagador)
        //        .OnDelete(DeleteBehavior.Restrict);
    }
}
