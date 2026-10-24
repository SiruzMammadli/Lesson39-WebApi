using Microsoft.EntityFrameworkCore;
using Web.Domain.Entities;

namespace Web.Domain;

public sealed class AppDbContext(
    DbContextOptions<AppDbContext> opts,
    IConfiguration config
) : DbContext(opts)
{
    public DbSet<Book> Books { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(config.GetConnectionString("Default"));
    }
}