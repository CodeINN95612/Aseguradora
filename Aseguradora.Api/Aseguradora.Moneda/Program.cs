using Aseguradora.Domain.Abstractions.Repositories;
using Aseguradora.Infrastructure;
using Aseguradora.Models.MonedaModel;
using Microsoft.Extensions.Configuration;

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

app.MapGet("/", async (IMonedaRepository monedaRepo) =>
{
    var monedas = await monedaRepo.GetAll();
    return Results.Ok(monedas.Select(p => new GetMonedaResponse(p.Id, p.Codigo, p.Nombre)));
}).WithName("GetAll").WithOpenApi();



app.Run();
