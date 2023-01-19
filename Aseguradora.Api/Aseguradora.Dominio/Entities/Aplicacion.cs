using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aseguradora.Domain.Entities;

[Table("Aplicacion")]
public class Aplicacion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int Numero { get; set; }
    public DateTime Fecha { get; set; }
    public int IdEmpresaAsegurado { get; set; }
    public int IdEmpresaPagador { get; set; }
    public string Desde { get; set; } = null!;
    public string Hasta { get; set; } = null!;
    public DateTime? FechaEmbarque { get; set; }
    public DateTime? FechaLLegada { get; set; }

    [ForeignKey("IdEmpresaAsegurado")]
    public virtual Empresa EmpresaAsegurado { get; set; } = null!;

    [ForeignKey("IdEmpresaPagador")]
    public virtual Empresa EmpresaPagador { get; set; } = null!;
};