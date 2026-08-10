using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace eCommerce.Models;

/// <summary>
/// Represents a product sold on Zac's Smoke Shop.
/// </summary>
public class Product
{

    /// <summary>
    /// Gets or sets the unique identifier for the product.
    /// </summary>
    [Key]
    public int ProductId { get; set; }

    /// <summary>
    /// Gets or sets the title of the product that the consumer will see.
    /// </summary>
    [Required]
    [StringLength(50, ErrorMessage = "Title cannot exceed 50 characters.")]
    public required string Title { get; set; }
    
    /// <summary>
    /// Gets or sets the current sales price of a product sold on Zac's Smoke Shop.
    /// </summary>
    [Range(0, 10_000, ErrorMessage = "Price must be between 0 and 10,000.")]
    [Precision(18, 2)]
    public decimal Price { get; set; }
    
    /// <summary>
    /// Gets or sets the description of the product.
    /// </summary>
    public string? Description { get; set; }
}

// add migration after creating new columns in the Product model using the following command in the Package Manager Console:
// PM> Add-Migration AddNewColumnsToProductModel