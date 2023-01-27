using Aseguradora.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aseguradora.Infrastructure.Data.Configurations;

public class AplicacionAduanaConfiguration : IEntityTypeConfiguration<AplicacionAduana>
{
    public void Configure(EntityTypeBuilder<AplicacionAduana> builder)
    {
        builder.Property(a => a.PesoBruto).HasPrecision(18, 5);
        builder.Property(a => a.MontoTotal).HasPrecision(18, 5);
        builder.Property(a => a.PorcentajeOtrosGastos).HasPrecision(18, 5);
        builder.Property(a => a.SumaAseguradora).HasPrecision(18, 5);
        builder.Property(a => a.ValorPrima).HasPrecision(18, 5);
    }
}
