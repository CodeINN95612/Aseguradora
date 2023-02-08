using Aseguradora.Domain.Abstractions.Repositories;
using Aseguradora.Domain.Entities;
using Aseguradora.Infrastructure;
using Aseguradora.Models.AplicacionModel;
using Microsoft.Extensions.Configuration;

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

app.MapGet("/", async (IAplicacionRepository appRepo) =>
{
    var aplicaciones = await appRepo.GetAll();

    return Results.Ok(aplicaciones
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
}).WithName("GetAll").WithOpenApi();

app.MapGet("/all", async (IAplicacionRepository appRepo, int idEmpresa) =>
{
    var aplicaciones = await appRepo.GetAll();

    return Results.Ok(aplicaciones
            .Where(a => a.IdEmpresa == idEmpresa)
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
        a.Estado is null ? "Ingresada" : ((bool)a.Estado ? "Aprobada" : "Negada")))
    );
}).WithName("GetAllByEmpresa").WithOpenApi();

app.MapGet("/ingresadas", async (IAplicacionRepository appRepo) =>
{
    var aplicaciones = await appRepo.GetAllIngresadas();

    return Results.Ok(aplicaciones
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
}).WithName("GetAllIngresadas").WithOpenApi();

app.MapGet("/getById", async (IAplicacionRepository appRepo, int id) =>
{
    if (await appRepo.GetById(id) is not Aplicacion ap)
    {
        return Results.BadRequest("Aplicacion no existe");
    }

    return Results.Ok(new GetFullAplicacionResponse(
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
}).WithName("GetById").WithOpenApi();

app.Run();
