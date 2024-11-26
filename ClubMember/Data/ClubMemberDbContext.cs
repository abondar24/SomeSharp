using Microsoft.EntityFrameworkCore;
using ClubMember.Models;

namespace ClubMember.Data;

public class ClubMemberDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={AppDomain.CurrentDomain.BaseDirectory}ClubDb");

        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<User> Users { get; set; }
}