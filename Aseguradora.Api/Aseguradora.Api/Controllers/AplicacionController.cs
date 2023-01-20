using Aseguradora.Api.Models.AplicacionModel;
using Aseguradora.Api.Models.UsuarioModel;
using Aseguradora.Domain.Abstractions.Repositories;
using Aseguradora.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Aseguradora.Api.Controllers;

[Route("api/[controller]")]
public class AplicacionController : AseguradoraController
{
    private IAplicacionRepository _aplicacionRepo;
    private IUsuarioRepository _userRepo;

    public AplicacionController(IAplicacionRepository aplicacionRepo, IUsuarioRepository userRepo)
    {
        _aplicacionRepo = aplicacionRepo;
        _userRepo = userRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var token = await HttpContext.GetTokenAsync("access_token");
        if (token is null)
        {
            return BadRequest("Invalid Token, Not logged In");
        }

        var securityTokenHandler = new JwtSecurityTokenHandler();
        if (!securityTokenHandler.CanReadToken(token))
        {
            return BadRequest("Invalid Token, Not logged In");
        }
        var decriptedToken = securityTokenHandler.ReadJwtToken(token);
        var claims = decriptedToken.Claims;
        string username = claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name)?.Value ?? "";

        if (await _userRepo.GetByUsername(username) is not Usuario usuarioExistente)
        {
            return BadRequest("Usuario no existe");
        }

        if (usuarioExistente.Empresa is null)
        {
            return Ok(new { });
        }

        var aplicaciones = await _aplicacionRepo.GetAll(usuarioExistente.Empresa.Id);

        return Ok(aplicaciones
            .Select(a => new GetAplicacionResponse(
                a.Id,
                a.Numero,
                a.Fecha,
                new(a.Usuario.Id, a.Usuario.UsuarioCampo, a.Usuario.Email),
                new(a.Empresa.Id, a.Empresa.Name, a.Empresa.Email, a.Empresa.RUC),
                a.Desde,
                a.Hasta,
                a.FechaEmbarque,
                a.FechaLLegada
        )));
    }

    [HttpGet("getById")]
    public async Task<IActionResult> GetById(int id)
    {
        if (await _aplicacionRepo.GetById(id) is not Aplicacion ap)
        {
            return BadRequest("Aplicacion no existe");
        }

        return Ok(new GetAplicacionResponse(
            ap.Id,
            ap.Numero,
            ap.Fecha,
            new(ap.Usuario.Id, ap.Usuario.UsuarioCampo, ap.Usuario.Email),
            new(ap.Empresa.Id, ap.Empresa.Name, ap.Empresa.Email, ap.Empresa.RUC),
            ap.Desde,
            ap.Hasta,
            ap.FechaEmbarque,
            ap.FechaLLegada
        ));
    }

    [HttpPost("search")]
    public async Task<IActionResult> Search(SearchAplicacionRequest request)
    {
        var token = await HttpContext.GetTokenAsync("access_token");
        if (token is null)
        {
            return BadRequest("Invalid Token, Not logged In");
        }

        var securityTokenHandler = new JwtSecurityTokenHandler();
        if (!securityTokenHandler.CanReadToken(token))
        {
            return BadRequest("Invalid Token, Not logged In");
        }
        var decriptedToken = securityTokenHandler.ReadJwtToken(token);
        var claims = decriptedToken.Claims;
        string username = claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name)?.Value ?? "";

        if (await _userRepo.GetByUsername(username) is not Usuario usuarioExistente)
        {
            return BadRequest("Usuario no existe");
        }
        var parametros = request.ParametroBusqueda.Split(" ");

        List<Aplicacion> aplicacionesEncontradas = new List<Aplicacion>();
        foreach (var parametro in parametros)
        {
            var aps = await _aplicacionRepo.FindByparameter(parametro);
            foreach (var ap in aps)
            {
                if (!aplicacionesEncontradas.Contains(ap))
                {
                    aplicacionesEncontradas.Add(ap);
                }
            }
        }

        return Ok(aplicacionesEncontradas.Select(a => new GetAplicacionResponse(
            a.Id,
            a.Numero,
            a.Fecha,
            new(a.Usuario.Id, a.Usuario.UsuarioCampo, a.Usuario.Email),
            new(a.Empresa.Id, a.Empresa.Name, a.Empresa.Email, a.Empresa.RUC),
            a.Desde,
            a.Hasta,
            a.FechaEmbarque,
            a.FechaLLegada)
        ));
    }

    [HttpPost("save")]
    public async Task<IActionResult> Save(SaveAplicacionRequest request)
    {
        var token = await HttpContext.GetTokenAsync("access_token");
        if (token is null)
        {
            return BadRequest("Invalid Token, Not logged In");
        }

        var securityTokenHandler = new JwtSecurityTokenHandler();
        if (!securityTokenHandler.CanReadToken(token))
        {
            return BadRequest("Invalid Token, Not logged In");
        }
        var decriptedToken = securityTokenHandler.ReadJwtToken(token);
        var claims = decriptedToken.Claims;
        string username = claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name)?.Value ?? "";

        if (await _userRepo.GetByUsername(username) is not Usuario usuarioExistente)
        {
            return BadRequest("Usuario no existe");
        }

        if (usuarioExistente.Empresa is null)
        {
            return BadRequest("El usuario no tiene empresa asignada");
        }

        if (string.IsNullOrEmpty(request.Desde) || string.IsNullOrEmpty(request.Hasta))
        {
            return BadRequest("Los campos Desde y Hasta son obligatorios");
        }

        return Ok(await _aplicacionRepo.Save(new()
        {
            Id = request.Id,
            Desde = request.Desde,
            Hasta = request.Hasta,
            Usuario = usuarioExistente,
            IdUsuario = usuarioExistente.Id,
            IdEmpresa = usuarioExistente.Empresa.Id,
            Empresa = usuarioExistente.Empresa,
            Fecha = request.Fecha,
            FechaEmbarque = request.FechaEmbargue,
            FechaLLegada = request.FechaLLegada,
        }));
    }
}
