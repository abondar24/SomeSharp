using GameStore.Data;
using GameStore.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(connection);


var app = builder.Build();

app.MapGameEndpoints();

await app.MigrateDb();

app.Run();

//TODO: add swagger