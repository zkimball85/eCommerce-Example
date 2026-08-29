using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Controllers;

/// <summary>
/// Represents the controller for managing products in the Zac's Smoke Shop application.
/// </summary>
public class ProductController : Controller
{
    private readonly ProductDbContext _context;

    public ProductController(ProductDbContext context)
    {
        _context = context;
    }

    private const int PageSize = 3;

    /// <summary>
    /// Displays a list of products with optional search and price filtering, along with pagination.
    /// </summary>
    /// <param name="searchString">The search string to filter products by title.</param>
    /// <param name="minPrice">The minimum price to filter products by.</param>
    /// <param name="maxPrice">The maximum price to filter products by.</param>
    /// <param name="page">The page number to display (default is 1).</param>
    /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
    public async Task<IActionResult> Index(string searchString, decimal? minPrice, decimal? maxPrice, int page = 1)
    {
        if (page < 1)
            page = 1;

        // Start building the query but DO NOT execute it yet
        IQueryable<Product> query = _context.Products.AsNoTracking();

        // Apply our filters to the query if the user typed anything in
        if (!string.IsNullOrEmpty(searchString))
        {
            query = query.Where(p => p.Title.Contains(searchString));
        }

        if (minPrice.HasValue)
        {
            query = query.Where(p => p.Price >= minPrice.Value);
        }

        if (maxPrice.HasValue)
        {
            query = query.Where(p => p.Price <= maxPrice.Value);
        }

        // Get the total count of the FILTERED products
        int totalProducts = await query.CountAsync();
        int totalPages = (int)Math.Ceiling(totalProducts / (double)PageSize);

        if (totalPages > 0 && page > totalPages)
        {
            page = totalPages;
        }

        // Now execute the query to get the current page of filtered products
        List<Product> products = await query
            .OrderBy(p => p.Title)
            .Skip((page - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync();

        // Pass pagination info
        ViewData["CurrentPage"] = page;
        ViewData["TotalPages"] = totalPages;
        ViewData["TotalProducts"] = totalProducts;

        // Pass the search parameters back to the view so the input boxes don't clear out
        ViewData["SearchString"] = searchString;
        ViewData["MinPrice"] = minPrice;
        ViewData["MaxPrice"] = maxPrice;

        return View(products);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    /// <summary>
    /// Handles the POST request to create a new product in the database.
    /// </summary>
    /// <param name="product">The product to create.</param>
    /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Product product)
    {
        if (ModelState.IsValid)
        {
            // Save the product to the database
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Show a success message to the user
            TempData["SuccessMessage"] = $"{product.Title} created successfully!";

            // Redirect to the product list page
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    /// <summary>
    /// Handles the GET request to display the edit form for a product.
    /// </summary>
    /// <param name="id">The ID of the product to edit.</param>
    /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
    [HttpGet]

    public async Task<IActionResult> Edit(int id)
    {
        // Retrieve the product from the database
        Product? product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    /// <summary>
    /// Handles the POST request to update a product in the database.
    /// </summary>
    /// <param name="product">The product to update.</param>
    /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> Edit(Product product)
    {
        if (ModelState.IsValid)
        {
            
            // Update the product in the database
            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            // Show a success message to the user
            TempData["SuccessMessage"] = $"{product.Title} updated successfully!";

            // Redirect to the product list page
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    /// <summary>
    /// Handles the GET request to display the delete confirmation page for a product.
    /// </summary>
    /// <param name="id">The ID of the product to delete.</param>
    /// <returns>A <see cref="IActionResult"/> representing the result of the operation.</returns>
    [HttpGet]

    public async Task<IActionResult> Delete(int id)
    {
        // Retrieve the product from the database
        Product? product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    /// <summary>
    /// Handles the POST request to delete a product from the database.
    /// </summary>
    /// <param name="id">The ID of the product to delete.</param>
    /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    [ActionName(nameof(Delete))]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        // Retrieve the product from the database
        Product? product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return RedirectToAction(nameof(Index));
        }

        // Remove the product from the database
        _context.Remove(product);
        await _context.SaveChangesAsync();

        // Show a success message to the user
        TempData["SuccessMessage"] = $"{product.Title} deleted successfully!";

        // Redirect to the product list page
        return RedirectToAction(nameof(Index));
    }
}