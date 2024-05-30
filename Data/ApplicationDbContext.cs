using Microsoft.EntityFrameworkCore;
using PharmaApi.Models;

namespace PharmaApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProductModel> Products { get; set; }
        public DbSet<SaleModel> Sales { get; set; }
        public DbSet<CloseSales> CloseSales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductModel>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<SaleModel>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<CloseSales>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<SaleModel>()
                .HasMany(s => s.Ticket)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CloseSales>()
                .HasMany(c => c.listado_de_ventas_dia)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
