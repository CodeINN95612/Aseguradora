using Aseguradora.Api.Models.EmpresaModel;
using Aseguradora.Api.Models.RolModel;

namespace Aseguradora.Api.Models.UsuarioModel;

public record GetUserResponse(int Id, string Username, string Email, GetRolResponse rol, GetEmpresaResponse? empresa);
