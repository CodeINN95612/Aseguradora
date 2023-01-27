using Aseguradora.Api.Models.AuthModel;
using Aseguradora.Api.Models.UsuarioModel;
using Aseguradora.Domain.Abstractions.Common;
using Aseguradora.Domain.Abstractions.Repositories;
using Aseguradora.Domain.Entities;
using Aseguradora.Shared.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aseguradora.Api.Controllers;

[Route("api/[controller]")]
public class AuthenticationController : AseguradoraController
{
    private IUsuarioRepository _userRepo;
    private IMonedaRepository _monedaRepo;
    private IJwtTokenGenerator _jwt;

    public AuthenticationController(IUsuarioRepository userRepo, IMonedaRepository monedaRepo, IJwtTokenGenerator jwt)
    {
        _userRepo = userRepo;
        _monedaRepo = monedaRepo;
        _jwt = jwt;
    }

    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginRequest login)
    {
        if (await _userRepo.GetByUnique(login.UsuarioEmail) is not Usuario usuarioExistente)
        {
            return BadRequest("Invalid Credentials");
        }

        if (usuarioExistente.Clave != login.Clave)
        {
            return BadRequest("Invalid Credentials");
        }

        if (await _monedaRepo.GetById(login.IdMoneda) is not Moneda monedaExistente)
        {
            return Problem("Moneda not found");
        }

        string token = _jwt.Generate(usuarioExistente);

        return Ok(new AuthenticatedUserResponse(
            new(usuarioExistente.Id, 
                usuarioExistente.UsuarioCampo, 
                usuarioExistente.Email,
                new(usuarioExistente.Rol.Id, 
                    usuarioExistente.Rol.Nombre,
                    usuarioExistente.Rol.EsAdministrador,
                    usuarioExistente.Rol.EsEjecutivo,
                    usuarioExistente.Rol.EsTrabajador),
                usuarioExistente.Empresa is null ? null : new(
                    usuarioExistente.Empresa.Id, 
                    usuarioExistente.Empresa.Name,
                    usuarioExistente.Empresa.Email,
                    usuarioExistente.Empresa.RUC)
            ),
            token,
            new(monedaExistente.Id, monedaExistente.Codigo, monedaExistente.Nombre)
        ));
    }
}
