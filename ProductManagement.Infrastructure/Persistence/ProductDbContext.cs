using Microsoft.EntityFrameworkCore;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Infrastructure.Persistence
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<LowStockLog> LowStockLogs { get; set; }

    }
}
