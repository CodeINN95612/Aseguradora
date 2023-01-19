using Aseguradora.Api.Models.AuthModel;
using Aseguradora.Auth.Data.Entities;
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

    public AuthenticationController(IUsuarioRepository userRepo, IMonedaRepository monedaRepo, IJwtTokenGenerator _jtw)
        :base(_jtw)
    {
        _userRepo = userRepo;
        _monedaRepo = monedaRepo;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userRepo.GetAll();
        return Ok(users.Select(u => new GetUserResponse(u.UsuarioCampo)));
    }

    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginRequest login)
    {
        if (await _userRepo.GetByUsername(login.Usuario) is not Usuario usuarioExistente)
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

        return Ok(new AuthenticatedUser(
            usuarioExistente.UsuarioCampo,
            token,
            usuarioExistente.IdRol,
            monedaExistente.Id
        ));
    }

    [HttpPut]
    [Route("saveUser")]
    public async Task<IActionResult> SaveUser(CreateUsuarioRequest usuario)
    {
        if (string.IsNullOrEmpty(usuario.Usuario) || string.IsNullOrEmpty(usuario.Clave))
        {
            return BadRequest("Usuario y contraseña obligatorios.");
        }

        if(await _userRepo.GetByUsername(usuario.Usuario) is Usuario)
        {
            return BadRequest("Already Exists");
        }

        return Ok(await _userRepo.Save(new()
        {
            UsuarioCampo = usuario.Usuario,
            Clave = usuario.Clave,
            IdRol = usuario.idRol,
            IdEmpresa = usuario.idEmpresa
        }));
    }

}
