using System.ComponentModel.DataAnnotations.Schema;

namespace Aseguradora.Auth.Data.Entities;

[Table("Usuario")]
public class Usuario
{
    public int Id { get; set; }

    [Column("Usuario")]
    public required string UsuarioCampo { get; set; }
    public required string Clave { get; set; }
}
