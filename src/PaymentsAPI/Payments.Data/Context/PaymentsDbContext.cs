using Microsoft.EntityFrameworkCore;
using Payments.Domain;

namespace Payments.Data.Context;

public class PaymentsDbContext : DbContext
{

    public PaymentsDbContext(DbContextOptions<PaymentsDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(o => o.Id);

            entity.Property(o => o.Number)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

            entity.HasIndex(o => o.Number).IsUnique();

            entity.Property(o => o.PricePaid)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

            entity.Property(o => o.GatewayTransactionId)
            .IsRequired(false)
            .HasMaxLength(100);
        });
    }

    public DbSet<Order> Orders { get; set; }
}
