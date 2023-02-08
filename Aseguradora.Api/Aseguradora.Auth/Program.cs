using Aseguradora.Domain.Abstractions.Common;
using Aseguradora.Domain.Abstractions.Repositories;
using Aseguradora.Domain.Entities;
using Aseguradora.Infrastructure;
using Aseguradora.Models.AuthModel;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(opts =>
{
    opts.AddPolicy(name: "Cors", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
app.UseCors("Cors");

app.MapPost("/login", async (IUsuarioRepository userRepo, IMonedaRepository monedaRepo, IJwtTokenGenerator jwt, LoginRequest login) =>
{
    if (await userRepo.GetByUnique(login.UsuarioEmail) is not Usuario usuarioExistente)
    {
        return Results.BadRequest("Invalid Credentials");
    }

    if (usuarioExistente.Clave != login.Clave)
    {
        return Results.BadRequest("Invalid Credentials");
    }

    if (await monedaRepo.GetById(login.IdMoneda) is not Moneda monedaExistente)
    {
        return Results.Problem("Moneda not found");
    }

    string token = jwt.Generate(usuarioExistente);

    return Results.Ok(new AuthenticatedUserResponse(
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
}).WithName("Login").WithOpenApi();

app.Run();
