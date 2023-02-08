using Aseguradora.Models.EmpresaModel;
using Aseguradora.Models.UsuarioModel;

namespace Aseguradora.Models.AplicacionModel;

public record GetAplicacionResponse(
    int Id,
    int Numero,
    DateTime Fecha,
    GetSimpleUserResponse Usuario,
    GetEmpresaResponse Empresa,
    string Desde,
    string Hasta,
    DateTime? FechaEmbargue,
    DateTime? FechaLLegada,
    string Estado);
