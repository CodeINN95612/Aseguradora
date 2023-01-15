using Aseguradora.Auth.Data.Entities;
using Aseguradora.Authentication.Models;
using Aseguradora.Domain.Abstractions.Common;
using Aseguradora.Domain.Abstractions.Repositories;
using Aseguradora.Infrastructure;
using Aseguradora.Shared.Authentication;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddAuthorization();
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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Cors");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

#region Methods

app.MapPut("/saveUser", (IUsuarioRepository repo, CreateUserRequest usuario) =>
{
    if(string.IsNullOrEmpty(usuario.Usuario) || string.IsNullOrEmpty(usuario.Clave))
    {
        return Results.BadRequest("Usuario y contraseña obligatorios.");
    }

    return Results.Ok(repo.SaveUser(new()
    {
        UsuarioCampo = usuario.Usuario,
        Clave = usuario.Clave
    }));
})
    .WithName("SaveUser")
    .WithOpenApi();

app.MapPost("/login", async (IUsuarioRepository repo, IJwtTokenGenerator jwt, CreateUserRequest usuario) =>
{
    if(await repo.GetByUsername(usuario.Usuario) is not Usuario usuarioExistente )
    {
        return Results.BadRequest("Invalid Credentials");
    }

    if(usuarioExistente.Clave != usuario.Clave)
    {
        return Results.BadRequest("Invalid Credentials");
    }

    string token = jwt.Generate(usuarioExistente);

    return Results.Ok(new AuthenticatedUser(
        usuarioExistente.UsuarioCampo,
        "Test",
        "Test"
    ));
})
    .WithName("Login")
    .WithOpenApi();

#endregion

app.Run();
