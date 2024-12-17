using GameStore.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGameEndpoints();

app.Run();

//TODO: add swagger
//TODO: add pagination to /games