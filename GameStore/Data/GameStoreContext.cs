using GameStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{

    public DbSet<Game> Games => Set<Game>();

    public DbSet<Genre> Genres => Set<Genre>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().HasData(
            new
            {
                Id = 1,
                Name = "FPS"
            },
             new
             {
                 Id = 2,
                 Name = "Racing"
             },
              new
              {
                  Id = 3,
                  Name = "Action"
              }
        );
    }


}