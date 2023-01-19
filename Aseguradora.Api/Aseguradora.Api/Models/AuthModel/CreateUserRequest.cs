namespace Aseguradora.Api.Models.AuthModel;

public record CreateUsuarioRequest(
    string Usuario,
    string Clave,
    int idRol,
    int? idEmpresa
);