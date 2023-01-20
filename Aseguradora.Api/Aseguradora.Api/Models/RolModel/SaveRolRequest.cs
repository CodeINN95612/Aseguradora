namespace Aseguradora.Api.Models.RolModel;

public record SaveRolRequest(int Id, string Nombre, bool EsAdmin, bool EsEjecutivo, bool EsTrabajador);
