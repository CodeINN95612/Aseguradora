using Aseguradora.Domain.Abstractions.Repositories;
using Aseguradora.Domain.Entities;
using Aseguradora.Infrastructure;
using Aseguradora.Models.EmpresaModel;
using Azure.Core;

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

app.MapGet("/", async (IEmpresaRepository empresaRepo) => 
{
    var empresas = await empresaRepo.GetAll();
    return Results.Ok(empresas.Select(e => new GetEmpresaResponse(e.Id, e.Name, e.Email, e.RUC)));
}).WithName("GetAll").WithOpenApi();

app.MapGet("/getById", async (IEmpresaRepository empresaRepo, int id) =>
{
    if (await empresaRepo.GetById(id) is not Empresa empresa)
    {
        return Results.NotFound("Does not exist");
    }
    return Results.Ok(new GetEmpresaResponse(empresa.Id, empresa.Name, empresa.Email, empresa.RUC));
}).WithName("GetById").WithOpenApi();

app.MapGet("/save", async (IEmpresaRepository empresaRepo, SaveEmpresaRequest request) => 
{
    if (string.IsNullOrEmpty(request.NombreEmpresa) ||
            string.IsNullOrEmpty(request.Email) ||
            string.IsNullOrEmpty(request.RUC))
    {
        return Results.BadRequest("Campos Nombre, Email y Ruc obligatorios");
    }

    return Results.Ok(await empresaRepo.Save(new Empresa
    {
        Id = request.Id,
        Name = request.NombreEmpresa,
        Email = request.Email,
        RUC = request.RUC
    }));
}).WithName("Save").WithOpenApi();

app.Run();
