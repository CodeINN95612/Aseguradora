using Aseguradora.Api.Models.EmpresaModel;
using Aseguradora.Api.Models.UsuarioModel;

namespace Aseguradora.Api.Models.AplicacionModel;

public record GetAplicacionResponse(
    int Id, 
    int Numero,
    DateTime Fecha,
    GetSimpleUserResponse Usuario,
    GetEmpresaResponse Empresa,
    string Desde,
    string Hasta,
    DateTime? FechaEmbargue,
    DateTime? FechaLLegada);
