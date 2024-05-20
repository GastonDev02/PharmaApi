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
            modelBuilder.Entity<ProductModel>().HasKey(p => p.id_producto);
            modelBuilder.Entity<SaleModel>().HasKey(s => s.id_venta);
            modelBuilder.Entity<CloseSales>().HasKey(cs => cs.id_cierre_venta);

            modelBuilder.Entity<SaleModel>()
             .HasOne(s => s.Product)
             .WithMany()
             .HasForeignKey(s => s.id_producto)
             .IsRequired(false); // Hace que la relación sea opcional
        }
    }
}
