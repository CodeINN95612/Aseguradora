using Aseguradora.Api.Models.AplicacionModel;
using Aseguradora.Api.Models.UsuarioModel;
using Aseguradora.Domain.Abstractions.Repositories;
using Aseguradora.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
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
        var aplicaciones = await _aplicacionRepo.GetAll();

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
                a.FechaLLegada,
                a.Estado is null ? "Ingresada" : ((bool)a.Estado ? "Aprobada" : "Negada")
        )));
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllByEmpresa(int idEmpresa)
    {
        var aplicaciones = await _aplicacionRepo.GetAll();

        return Ok(aplicaciones.Where(a => a.IdEmpresa == idEmpresa).Select(a => new GetAplicacionResponse(
            a.Id,
            a.Numero,
            a.Fecha,
            new(a.Usuario.Id, a.Usuario.UsuarioCampo, a.Usuario.Email),
            new(a.Empresa.Id, a.Empresa.Name, a.Empresa.Email, a.Empresa.RUC),
            a.Desde,
            a.Hasta,
            a.FechaEmbarque,
            a.FechaLLegada,
            a.Estado is null ? "Ingresada" : ((bool)a.Estado ? "Aprobada" : "Negada")))
        );
    }

    [HttpGet("ingresadas")]
    public async Task<IActionResult> GetAllIngresadas()
    {
        var aplicaciones = await _aplicacionRepo.GetAllIngresadas();

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
                a.FechaLLegada,
                "Ingresada"
        )));
    }

    [HttpGet("getById")]
    public async Task<IActionResult> GetById(int id)
    {
        if (await _aplicacionRepo.GetById(id) is not Aplicacion ap)
        {
            return BadRequest("Aplicacion no existe");
        }

        return Ok(new GetFullAplicacionResponse(
            ap.Id,
            ap.Numero,
            ap.Fecha,
            new(ap.Usuario.Id, ap.Usuario.UsuarioCampo, ap.Usuario.Email),
            new(ap.Empresa.Id, ap.Empresa.Name, ap.Empresa.Email, ap.Empresa.RUC),
            ap.Desde,
            ap.Hasta,
            ap.TipoTransporte,
            ap.Perteneciente,
            ap.Consignada,
            ap.EmbarcadoPor,
            ap.FechaEmbarque,
            ap.FechaLLegada,
            ap.NotaPredio,
            ap.OrdenCompra,
            new(
                ap.Aduana.IdAplicacion,
                ap.Aduana.Item,
                ap.Aduana.Marca,
                ap.Aduana.Numero,
                ap.Aduana.PesoBruto,
                ap.Aduana.Bultos,
                ap.Aduana.MontoTotal,
                ap.Aduana.PorcentajeOtrosGastos,
                ap.Aduana.SumaAseguradora,
                ap.Aduana.ValorPrima,
                ap.Aduana.DescripcionContenido,
                ap.Aduana.Observaciones),
            ap.Estado is null ? "Ingresada" : ((bool)ap.Estado ? "Aprobada" : "Negada")
        ));
    }

    [HttpPost("search")]
    public async Task<IActionResult> Search(SearchAplicacionRequest request)
    {
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
            a.FechaLLegada,
            a.Estado is null ? "Ingresada" : ((bool)a.Estado ? "Aprobada" : "Negada"))
        ));
    }

    [HttpPost("ingresar")]
    public async Task<IActionResult> Ingresar(SaveAplicacionRequest request)
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
            Desde = request.Desde,
            Hasta = request.Hasta,
            Consignada = request.Consignada,
            EmbarcadoPor = request.EmbarcadoPor,
            NotaPredio = request.NotaPredio,
            OrdenCompra = request.OrdenCompra,
            Perteneciente = request.Perteneciente,
            TipoTransporte = request.TipoTransporte,
            Usuario = usuarioExistente,
            IdUsuario = usuarioExistente.Id,
            IdEmpresa = usuarioExistente.Empresa.Id,
            Empresa = usuarioExistente.Empresa,
            Fecha = request.Fecha,
            FechaEmbarque = request.FechaEmbargue,
            FechaLLegada = request.FechaLLegada,
            Estado = null,
            Aduana = new()
            {
                Numero = request.Aduana.Numero,
                Bultos = request.Aduana.Bultos,
                DescripcionContenido = request.Aduana.DescripcionContenido,
                Item = request.Aduana.Item,
                Marca = request.Aduana.Marca,
                MontoTotal = request.Aduana.MontoTotal,
                Observaciones = request.Aduana.Observaciones,
                PesoBruto = request.Aduana.PesoBruto,
                PorcentajeOtrosGastos = request.Aduana.PesoBruto,
                SumaAseguradora = request.Aduana.SumaAseguradora,
                ValorPrima = request.Aduana.ValorPrima
            }
        }));
    }

    [HttpPost("aprobar")]
    public async Task<IActionResult> Aprobar(int IdAplicacion)
    {
        var app = await _aplicacionRepo.GetById(IdAplicacion);
        if (app is null)
        {
            return BadRequest("Id Invalida");
        }
        app.Estado = true;
        return Ok(await _aplicacionRepo.Save(app));
    }

    [HttpPost("negar")]
    public async Task<IActionResult> Negar(int IdAplicacion)
    {
        var app = await _aplicacionRepo.GetById(IdAplicacion);
        if (app is null)
        {
            return BadRequest("Id Invalida");
        }
        app.Estado = false;
        return Ok(await _aplicacionRepo.Save(app));
    }
}
