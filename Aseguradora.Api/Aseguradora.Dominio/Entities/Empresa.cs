using Aseguradora.Auth.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aseguradora.Domain.Entities;

[Table("Empresa")]
public class Empresa
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public required string Name { get; set; }
    public virtual required ICollection<Usuario> Usuarios { get; set; }
    public virtual required ICollection<Aplicacion> Aplicaciones { get; set; }
};