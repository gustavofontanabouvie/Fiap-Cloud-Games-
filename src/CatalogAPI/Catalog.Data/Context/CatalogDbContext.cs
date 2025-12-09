using Catalog.Domain;
using Microsoft.EntityFrameworkCore;


namespace Catalog.Data.Context
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Game>()
            .HasMany(g => g.Promotions)
            .WithMany(p => p.Games);


            modelBuilder.Entity<Game>()
                .Property(g => g.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Promotion>()
                .Property(p => p.DiscountPercentage)
                .HasColumnType("decimal(5,2)");
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
    }
}
