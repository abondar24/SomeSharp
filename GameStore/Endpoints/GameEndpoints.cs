using GameStore.Data;
using GameStore.DTOs;
using GameStore.Entities;
using GameStore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Endpoints;

public static class GameEndpoints
{

    const string GetGameEndpoint = "GetGame";


    public static RouteGroupBuilder MapGameEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games");

        group.MapGet("/", (int? offset, int? limit, GameStoreContext dbContext) =>
        {
            var actualOffset = offset ?? 0;
            var actualLimit = limit ?? 10;

            if (actualOffset < 0 || actualLimit <= 0)
            {
                return Results.BadRequest(new { Message = "Offset must be >= 0 and limit must be > 0." });
            }

            var paginatedGames = dbContext.Games
            .OrderBy(g => g.Id)
            .Skip(actualOffset)
            .Take(actualLimit)
            .Include(g => g.Genre)
            .Select(g => g.ToGameSummaryDto())
            .AsNoTracking()
            .ToList();


            return Results.Ok(paginatedGames);

        });

        group.MapGet("/{id}", (int id, GameStoreContext dbContext) =>
        {
            var game = dbContext.Games.Find(id);
            return game is null ? Results.NotFound() : Results.Ok(game.ToGameDetailsDto());

        }).WithName(GetGameEndpoint);

        group.MapPost("/", (CreateGameDto body, GameStoreContext dbContext) =>
        {
            var game = body.ToEntity();
            game.Genre = dbContext.Genres.Find(body.GenreId);
            dbContext.Games.Add(game);
            dbContext.SaveChanges();

            return Results.CreatedAtRoute(GetGameEndpoint, new { id = game.Id }, game.ToGameDetailsDto());
        })
        .WithParameterValidation();

        group.MapPut("/{id}", (int id, UpdateGameDto body, GameStoreContext dbContext) =>
       {
           var existingGame = dbContext.Games.Find(id);
           if (existingGame is null)
           {
               return Results.NotFound();
           }

           dbContext.Entry(existingGame)
           .CurrentValues
           .SetValues(body.ToEntity(id));

           dbContext.SaveChanges();

           return Results.NoContent();
       })
        .WithParameterValidation(); ;

        group.MapDelete("/{id}", (int id, GameStoreContext dbContext) =>
        {

            dbContext.Games
                     .Where(game => game.Id == id)
                     .ExecuteDelete();

            dbContext.SaveChanges();

            return Results.NoContent();
        });

        return group;
    }

}