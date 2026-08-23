using eCommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Data;

public class ProductDbContext(DbContextOptions options) : DbContext(options)
{
    public required DbContextOptions<ProductDbContext> options;


    /// <summary>
    /// Configures the model for the database context, ensuring that
    /// the Username and Email fields in the Member entity are unique.
    /// </summary>
    /// <param name="modelBuilder">The ModelBuilder instance.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Ensure the Username and Email fields are unique in the database
        modelBuilder.Entity<Member>().HasIndex(m => m.Username).IsUnique();
        modelBuilder.Entity<Member>().HasIndex(m => m.Email).IsUnique();
    }

    // Entities to be added to the database context
    public DbSet<Product> Products { get; set; }

    public DbSet<Member> Members { get; set; }
}
