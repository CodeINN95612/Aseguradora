using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aseguradora.Domain.Entities;

public class AplicacionAduana
{
    [Key]
    public int IdAplicacion { get; set; }
    public string Item { get; set; } = null!;
    public string Marca { get; set; } = null!;
    public string Numero { get; set; } = null!;
    public decimal PesoBruto { get; set; }
    public int Bultos { get; set; }
    public decimal MontoTotal { get; set; }
    public decimal PorcentajeOtrosGastos { get; set; }
    public decimal SumaAseguradora { get; set; }
    public decimal ValorPrima { get; set; }
    public string DescripcionContenido { get; set; } = null!;
    public string Observaciones { get; set; } = null!;


    [ForeignKey("IdAplicacion")]
    public virtual Aplicacion Aplicacion { get; set; } = null!;
}
