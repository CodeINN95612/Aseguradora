namespace Aseguradora.Api.Models.RolModel;

public record GetRolResponse(int Id, string Nombre, bool EsAdmin, bool EsEjecutivo, bool EsTrabajador);
