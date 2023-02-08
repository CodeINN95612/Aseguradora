namespace Aseguradora.Models.UsuarioModel;

public record SaveUserRequest(string Username, string Email, string Clave, int IdRol, int? IdEmpresa);
