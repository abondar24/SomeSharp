using GameStore.Data;
using GameStore.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connection = "Data Source=GameStore.db";
builder.Services.AddSqlite<GameStoreContext>(connection);


var app = builder.Build();

app.MapGameEndpoints();

app.Run();

//TODO: add swagger