using Microsoft.EntityFrameworkCore;
using ProductFilterApi.Models;

namespace ProductFilterApi.Data;

// Entity Framework DbContext for SQLite database operations
public class ProductContext : DbContext
{
    // Constructor accepting database configuration options
    public ProductContext(DbContextOptions<ProductContext> options) : base(options) { }
    
    // DbSet representing the Products table in the database
    public DbSet<Product> Products { get; set; }
}