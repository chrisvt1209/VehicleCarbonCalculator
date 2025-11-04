using Rdw.ApiWrapper.Extensions;
using Rdw.Repository.Extensions;
using Rdw.Service.Services;
using Rdw.Service.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddGrpc();

builder.Services.AddScoped<EmissionService>();

builder.Services.AddTransient<IFuelTypeConfig, FuelTypeConfig>();

builder.Services.AddRdwApiWrapper();

builder.Services.AddRepository(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.Services.ApplyMigration();
}

app.MapGrpcService<RdwDataService>();

app.Run();
