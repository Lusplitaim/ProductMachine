using ProductCatalog.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ProductCatalog.Infrastructure.Data
{
    internal class DatabaseContext : DbContext
    {
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<ProductBrandEntity> ProductBrands { get; set; }
        public DbSet<CoinEntity> Coins { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ProductEntity>(b =>
            {
                b.HasKey(p => p.Id);
                b.Property(p => p.Name).HasMaxLength(200);
                b.Property(p => p.MaxQuantity).HasDefaultValue(0);
                b.HasOne(p => p.Brand).WithMany(b => b.Products)
                    .HasForeignKey(p => p.BrandId);
            });

            builder.Entity<ProductBrandEntity>(b =>
            {
                b.HasKey(p => p.Id);
                b.Property(p => p.Name).HasMaxLength(200);
            });

            builder.Entity<CoinEntity>(b =>
            {
                b.HasKey(p => p.Nominal);
                b.Property(p => p.MaxQuantity).HasDefaultValue(0);
            });

            builder.Entity<OrderEntity>(b =>
            {
                b.HasKey(p => p.Id);
                b.Property(p => p.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });
        }
    }
}
