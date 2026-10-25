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
    public DbSet<UserBook> UserBooks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(config.GetConnectionString("Default"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            //entity.ToTable("users");
            entity.Property(e => e.Fullname).HasMaxLength(64);
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Password).HasMaxLength(256);
        });

        modelBuilder.Entity<UserBook>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.BookId });

            entity.HasOne(e => e.User)
            .WithMany(e => e.Books)
            .HasForeignKey(e => e.UserId);

            entity.HasOne(e => e.Book)
            .WithMany(e => e.Users)
            .HasForeignKey(e => e.BookId);
        });
    }
}