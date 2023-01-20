using Aseguradora.Api.Models.MonedaModel;
using Aseguradora.Domain.Abstractions.Common;
using Aseguradora.Domain.Abstractions.Repositories;
using Aseguradora.Infrastructure.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aseguradora.Api.Controllers;

[Route("api/[controller]")]
public class MonedaController : AseguradoraController
{
    private IMonedaRepository _repo;
    public MonedaController(IMonedaRepository repo)
    {
        _repo = repo;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var monedas = await _repo.GetAll();
        return Ok(monedas.Select(p => new MonedaGetAllResponse(p.Id, p.Codigo, p.Nombre)));
    }
}
