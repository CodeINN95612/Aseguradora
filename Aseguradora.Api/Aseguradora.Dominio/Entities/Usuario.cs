using Aseguradora.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aseguradora.Auth.Data.Entities;

[Table("Usuario")]
public class Usuario
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Column("Usuario")]
    public string UsuarioCampo { get; set; } = null!;
    public int? IdEmpresa { get; set; }
    public int IdRol { get; set; }
    public string Clave { get; set; } = null!;

    [ForeignKey("IdEmpresa")]
    public virtual Empresa? Empresa { get; set; }
    [ForeignKey("IdRol")]
    public virtual Rol Rol { get; set; } = null!;
}
