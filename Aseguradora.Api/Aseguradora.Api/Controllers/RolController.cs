using Aseguradora.Api.Models.RolModel;
using Aseguradora.Domain.Abstractions.Repositories;
using Aseguradora.Domain.Entities;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace Aseguradora.Api.Controllers;

[Route("api/[controller]")]
public class RolController : AseguradoraController
{
    private IRolRepository _rolRepo;

    public RolController(IRolRepository rolRepo)
    {
        _rolRepo = rolRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var roles = await _rolRepo.GetAll();
        return Ok(roles.Select(r => new GetRolResponse(r.Id, r.Nombre, r.EsAdministrador, r.EsEjecutivo, r.EsTrabajador)));
    }

    [HttpGet("getById")]
    public async Task<IActionResult> GetById(int id)
    {
        if(await _rolRepo.GetById(id) is not Rol rol)
        {
            return NotFound("No existe el rol");
        }

        return Ok(new GetRolResponse(rol.Id, rol.Nombre, rol.EsAdministrador, rol.EsEjecutivo, rol.EsTrabajador));
    }

    [HttpPost("save")]
    public async Task<IActionResult> Save(SaveRolRequest request)
    {
        if(string.IsNullOrEmpty(request.Nombre))
        {
            return BadRequest("El campo Nombre es obligatorio.");
        }

        bool[] permisos = { request.EsAdmin, request.EsEjecutivo, request.EsTrabajador };

        if(permisos.Where(p => p).Count() != 1)
        {
            return BadRequest("Debe de haber un maximo de uno de los campos EsAdmin, EsEjecutivo o EsTrabajador que debe ser verdadero");
        }

        return Ok(await _rolRepo.Save(new()
        {
            Id = request.Id,
            Nombre = request.Nombre,
            EsAdministrador = request.EsAdmin,
            EsEjecutivo = request.EsEjecutivo,
            EsTrabajador = request.EsTrabajador
        }));
    }
}
