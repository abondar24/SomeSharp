using GameStore.DTOs;

namespace GameStore.Endpoints;

public static class GameEndpoints
{
    private static readonly List<GameDto> games =
    [
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
    ];

    const string GetGameEndpoint = "GetGame";


    public static RouteGroupBuilder MapGameEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games");

        group.MapGet("/", () => games);

        group.MapGet("/{id}", (int id) =>
        {
            var game = games.Find(game => game.Id == id);

            return game is null ? Results.NotFound() : Results.Ok(game);

        }).WithName(GetGameEndpoint);

        group.MapPost("/", (CreateGameDto body) =>
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

        group.MapPut("/{id}", (int id, UpdateGameDto body) =>
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

        group.MapDelete("/{id}", (int id) =>
        {

            games.RemoveAll(game => game.Id == id);
            return Results.NoContent();
        });

        return group;
    }

}