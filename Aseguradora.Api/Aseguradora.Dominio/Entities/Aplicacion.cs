using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aseguradora.Domain.Entities;

[Table("Aplicacion")]
[Index("Numero", IsUnique = true)]
public class Aplicacion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int Numero { get; private set; }
    public DateTime Fecha { get; set; }
    public int IdUsuario { get; set; }
    public int IdEmpresa{ get; set; }
    public string Desde { get; set; } = null!;
    public string Hasta { get; set; } = null!;
    public string TipoTransporte { get; set; } = null!;
    public string Perteneciente { get; set; } = null!;
    public string Consignada { get; set; } = null!;
    public string EmbarcadoPor { get; set; } = null!;
    public DateTime? FechaEmbarque { get; set; }
    public DateTime? FechaLLegada { get; set; }
    public string NotaPredio { get; set; } = null!;
    public string OrdenCompra { get; set; } = null!;
    public bool? Estado { get; set; }

    [ForeignKey("IdEmpresa")]
    public virtual Empresa Empresa { get; set; } = null!;

    [ForeignKey("IdUsuario")]
    public virtual Usuario Usuario { get; set; } = null!;

    public virtual AplicacionAduana Aduana { get; set; } = null!;
};