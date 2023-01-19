using Aseguradora.Auth.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aseguradora.Domain.Entities;

[Table("Rol")]
public class Rol
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public bool EsTrabajador { get; set; }
    public bool EsEjecutivo { get; set; }
    public bool EsAdministrador { get; set; }
    public virtual ICollection<Usuario> Usuarios { get; set; } = null!;
};