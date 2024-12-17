using GameStore.DTOs;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


var games = new List<GameDto>
{
    new (
        1,
        "Asetto Corsa",
        "Racing",
        29.99M,
        new DateOnly(2014,5,6)
    ),
     new (
        2,
        "Battlefield 2042",
        "FPS",
        59.99M,
        new DateOnly(2021,11,17)
    )
};

const string GetGameEndpoint = "GetGame";

app.MapGet("games", () => games);

app.MapGet("games/{id}", (int id) =>
{
    var game = games.Find(game => game.Id == id);

    return game is null ? Results.NotFound() : Results.Ok(game);

}).WithName(GetGameEndpoint);

app.MapPost("games", (CreateGameDto body) =>
{

    var game = new GameDto(
           games.Count + 1,
            body.Name,
            body.Genre,
            body.Price,
            body.ReleaseDate
        );

    games.Add(game);

    return Results.CreatedAtRoute(GetGameEndpoint, new { id = game.Id }, game);
});

app.MapPut("games/{id}", (int id, UpdateGameDto body) =>
{
    var existingId = games.FindIndex(game => game.Id == id);

    if (existingId == -1)
    {
        return Results.NotFound();
    }

    games[existingId] = new GameDto(
            id,
            body.Name,
            body.Genre,
            body.Price,
            body.ReleaseDate
        );

    return Results.NoContent();
});

app.MapDelete("games/{id}", (int id) =>
{

    games.RemoveAll(game => game.Id == id);
    return Results.NoContent();
});


app.Run();
