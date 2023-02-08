using Aseguradora.Models.EmpresaModel;
using Aseguradora.Models.RolModel;

namespace Aseguradora.Models.UsuarioModel;

public record GetUserResponse(int Id, string Username, string Email, GetRolResponse rol, GetEmpresaResponse? empresa);
