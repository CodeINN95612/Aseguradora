using Aseguradora.Models.EmpresaModel;
using Aseguradora.Models.UsuarioModel;

namespace Aseguradora.Models.AplicacionModel;

public record GetFullAplicacionResponse(
    int Id,
    int Numero,
    DateTime Fecha,
    GetSimpleUserResponse Usuario,
    GetEmpresaResponse Empresa,
    string Desde,
    string Hasta,
    string TipoTransporte,
    string Perteneciente,
    string Consignada,
    string EmbarcadoPor,
    DateTime? FechaEmbarque,
    DateTime? FechaLLegada,
    string NotaPredio,
    string OrdenCompra,
    GetAplicacionAduanaResponse Aduana,
    string Estado);
