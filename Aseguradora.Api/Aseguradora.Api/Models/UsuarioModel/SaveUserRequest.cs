namespace Aseguradora.Api.Models.UsuarioModel;

public record SaveUserRequest(int Id, string Username, string Email, string Clave, int IdRol, int IdEmpresa);
