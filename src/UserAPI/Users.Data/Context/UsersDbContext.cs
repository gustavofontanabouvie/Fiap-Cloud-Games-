using Microsoft.EntityFrameworkCore;
using Users.Domain;

namespace Users.Data.Context;

public class UsersDbContext : DbContext
{
    public UsersDbContext(DbContextOptions<UsersDbContext> dbContextOptions) : base(dbContextOptions) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);
        // modelBuilder.ApplyConfigurationsFromAssembly(typeof())

        modelBuilder.Entity<UserLibrary>()
            .HasIndex(ul => new { ul.UserId, ul.GameId })
            .IsUnique();

        modelBuilder.Entity<UserLibrary>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(ul => ul.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UserLibrary>()
            .Property(ul => ul.PricePaid)
            .HasColumnType("decimal(18,2)");

    }

    public DbSet<User> Users { get; set; }

    public DbSet<UserLibrary> UsersLibrary { get; set; }
}
