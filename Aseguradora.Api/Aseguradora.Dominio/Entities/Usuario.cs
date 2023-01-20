using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aseguradora.Domain.Entities;

[Table("Usuario")]
[Index("UsuarioCampo", IsUnique = true)]
[Index("Email", IsUnique = true)]
public class Usuario
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Column("Usuario")]
    public string UsuarioCampo { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int? IdEmpresa { get; set; }
    public int IdRol { get; set; }
    public string Clave { get; set; } = null!;

    [ForeignKey("IdEmpresa")]
    public virtual Empresa? Empresa { get; set; }
    [ForeignKey("IdRol")]
    public virtual Rol Rol { get; set; } = null!;

    public virtual ICollection<Aplicacion> Aplicaciones { get; set; } = null!;

}
