using Aseguradora.Domain.Abstractions.Repositories;
using Aseguradora.Infrastructure;
using Aseguradora.Models.UsuarioModel;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(configuration);

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

//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
app.UseCors("Cors");

app.MapGet("/", async (IUsuarioRepository repo) =>
{
    var users = await repo.GetAll();
    return Results.Ok(users.Select(u => new GetUserResponse(
        u.Id,
        u.UsuarioCampo,
        u.Email,
        new(u.Rol.Id, u.Rol.Nombre, u.Rol.EsAdministrador, u.Rol.EsEjecutivo, u.Rol.EsTrabajador),
        u.Empresa is null ? null : new(u.Empresa.Id, u.Empresa.Name, u.Empresa.Email, u.Empresa.RUC)
    )));
}).WithName("GetAll").WithOpenApi();



app.Run();
