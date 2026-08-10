using eCommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Data;

public class ProductDbContext(DbContextOptions options) : DbContext(options)
{
    public required DbContextOptions<ProductDbContext> options;

    // Entities to be added to the database context
    public DbSet<Product> Products { get; set; }
}
