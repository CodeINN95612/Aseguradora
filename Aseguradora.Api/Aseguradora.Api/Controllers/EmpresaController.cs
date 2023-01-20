using Aseguradora.Api.Models.EmpresaModel;
using Aseguradora.Domain.Abstractions.Common;
using Aseguradora.Domain.Abstractions.Repositories;
using Aseguradora.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Aseguradora.Api.Controllers;

[Route("api/[controller]")]
public class EmpresaController : AseguradoraController
{
    private IEmpresaRepository _empresaRepo;
    public EmpresaController(IEmpresaRepository empresaRepo)
    {
        _empresaRepo = empresaRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var empresas = await _empresaRepo.GetAll();
        return Ok(empresas.Select(e => new GetEmpresaResponse(e.Id, e.Name, e.Email, e.RUC)));
    }

    [HttpGet]
    [Route("getById")]
    public async Task<IActionResult> GetById(int id)
    {
        if (await _empresaRepo.GetById(id) is not Empresa empresa)
        {
            return NotFound("Does not exist");
        }
        return Ok(new GetEmpresaResponse(empresa.Id, empresa.Name, empresa.Email, empresa.RUC));
    }

    [HttpPost]
    [Route("save")]
    public async Task<IActionResult> Save(SaveEmpresaRequest request)
    {
        if(string.IsNullOrEmpty(request.Name) ||
            string.IsNullOrEmpty(request.Email) ||
            string.IsNullOrEmpty(request.RUC))
        {
            return BadRequest("Campos Nombre, Email y Ruc obligatorios");
        }

        return Ok(await _empresaRepo.Save(new Empresa
        {
            Id = request.Id,
            Name = request.Name,
            Email = request.Email,
            RUC = request.RUC
        }));
    }
}
