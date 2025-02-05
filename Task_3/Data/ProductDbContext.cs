using Microsoft.EntityFrameworkCore;
using Task_3.Entities;

namespace Task_3.Data
{
    public class ProductDbContext:DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }
        public DbSet<Product>Products { get; set; }
    }
}
