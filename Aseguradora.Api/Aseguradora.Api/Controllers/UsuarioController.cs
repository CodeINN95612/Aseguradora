using Aseguradora.Api.Models.UsuarioModel;
using Aseguradora.Domain.Abstractions.Repositories;
using Aseguradora.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Aseguradora.Api.Controllers;

[Route("api/[controller]")]
public class UsuarioController : AseguradoraController
{
    private IUsuarioRepository _usuarioRepo;
    private IRolRepository _rolRepository;
    private IEmpresaRepository _empresaRepository;

    public UsuarioController(IUsuarioRepository usuarioRepo, IRolRepository rolRepository, IEmpresaRepository empresaRepository)
    {
        _usuarioRepo = usuarioRepo;
        _rolRepository = rolRepository;
        _empresaRepository = empresaRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _usuarioRepo.GetAll();
        return Ok(users.Select(u => new GetUserResponse(
            u.Id,
            u.UsuarioCampo,
            u.Email,
            new(u.Rol.Id, u.Rol.Nombre),
            u.Empresa is null ? null : new(u.Empresa.Id, u.Empresa.Name, u.Empresa.Email, u.Empresa.RUC)
        )));
    }

    [HttpGet("getByUsername")]
    public async Task<IActionResult> GetByUsername(string username)
    {
        if(await _usuarioRepo.GetByUsername(username) is not Usuario usuario)
        {
            return BadRequest("Usuario no existe");
        }

        return Ok(new GetUserResponse(
            usuario.Id,
            usuario.UsuarioCampo,
            usuario.Email,
            new(usuario.Rol.Id, usuario.Rol.Nombre),
            usuario.Empresa is null ? null : new(usuario.Empresa.Id, usuario.Empresa.Name, usuario.Empresa.Email, usuario.Empresa.RUC)
        ));
    }

    [HttpPost("save")]
    public async Task<IActionResult> Save(SaveUserRequest request)
    {
        if(string.IsNullOrEmpty(request.Username) ||
            string.IsNullOrEmpty(request.Clave))
        {
            return BadRequest("Campo Usuario y Clave es obligatorio");
        }

        if(await _rolRepository.GetById(request.IdRol) is not Rol rol)
        {
            return BadRequest("Campo IdRol no es valido.");
        }

        var empresa = await _empresaRepository.GetById(request.IdEmpresa);
        if(request.IdEmpresa is not 0 && empresa is null)
        {
            return BadRequest("Campo IdEmpresa no es valido.");
        }

        return Ok(await _usuarioRepo.Save(new()
        {
            Id = request.Id,
            UsuarioCampo = request.Username,
            Email = request.Email,
            Clave = request.Clave,
            Empresa = empresa,
            IdEmpresa = empresa?.Id ?? 0,
            Rol = rol,
            IdRol = rol.Id
        }));
    }
}
