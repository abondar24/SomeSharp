using GameStore.Data;
using GameStore.Endpoints;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(connection);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "GameStore API",
        Version = "v1",
        Description = "A simple example ASP.NET Core Web API with Swagger UI",
    });
});


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "GameStore API V1");
    c.RoutePrefix = string.Empty; // Serve Swagger UI at root
});

app.MapGameEndpoints();
app.MapGenreEndpoints();

await app.MigrateDb();

app.Run();