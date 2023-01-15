using System.ComponentModel.DataAnnotations.Schema;

namespace Aseguradora.Auth.Data.Entities;

[Table("Moneda")]
public class Moneda
{
    public required int Id { get; set; }
    public required string Codigo { get; set; }
    public required string Nombre { get; set; }
}
