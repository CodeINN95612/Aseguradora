using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aseguradora.Domain.Entities;

[Table("Empresa")]
[Index("RUC", IsUnique = true)]
[Index("Email", IsUnique = true)]
public class Empresa
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string RUC { get; set; } = null!;
    public string Email { get; set; } = null!;
    public virtual ICollection<Usuario> Usuarios { get; set; } = null!;
    public virtual ICollection<Aplicacion> Aplicaciones { get; set; } = null!;
};