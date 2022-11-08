global using Microsoft.AspNetCore.Mvc;
global using Newtonsoft.Json;
global using System;
global using System.Linq;
global using System.Net;
using FluentValidation;
using MaintenanceCheckinCheckout.API.Helpers;
using MaintenanceCheckinCheckout.Application.Interfaces.Service.UseCases.Car;
using MaintenanceCheckinCheckout.Application.Interfaces.Services.UseCases.Car;
using MaintenanceCheckinCheckout.Application.ViewModels.Car.Requests;
using MaintenanceCheckinCheckout.Infra.IoC;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region Configuracoes adicionadas - builder.services
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("V1", new OpenApiInfo() { Title = "API V1", Version = "V1.0" });
    //options.SwaggerDoc("V2", new OpenApiInfo() { Title = "API V2", Version = "V2.0" });
    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    options.CustomSchemaIds(x => x.FullName);
});

NativeInjector.RegisterServices(builder.Services);

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddApplicationInsightsTelemetry(options =>
{
    options.ConnectionString = builder.Configuration["ApplicationInsights:ConnectionString"];
});

builder.Services.AddMvc(options =>
{
    //options.Filters.Add(typeof(DomainExceptionFilter));
    options.Filters.Add(typeof(ValidateActionFilterAttribute));
});
#endregion

var app = builder.Build();

#region Configuracoes adicionadas - app

app.UseMiddleware(typeof(ApiExceptionMiddleware));
#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint($"/swagger/V1/swagger.json", "V1.0");
    });
}

app.UseHttpsRedirection();


#region Endpoints

app.MapGet("/car", async (IGetCarsUseCase getUseCase) =>
{
    app.Logger.LogInformation($"Obtendo todos os registros");

    var response = await getUseCase.Execute();

    return response;
});

app.MapPost("/car", async (CarRegisterRequest request, IRegisterCarUseCase registerUseCase) =>
{
    app.Logger.LogInformation($"Novo registro de carro solicitado", request);

    var response = await registerUseCase.Execute(request);

    return response;

});

app.MapPut("/car", async (CarUpdateRequest request, IUpdateCarUseCase updateUseCase) =>
{
    app.Logger.LogInformation($"Atualização de carro solicitado", request);

    var response = await updateUseCase.Execute(request);

    return Results.Ok(response);

});

app.MapDelete("/car/delete/{id}", async (Guid id, IDeleteCarUseCase deleteUsecase) =>
{
    app.Logger.LogInformation($"Remoção de carro solicitado", id);

    await deleteUsecase.Execute(id);

    return Results.Ok();
});


#endregion


app.Run();