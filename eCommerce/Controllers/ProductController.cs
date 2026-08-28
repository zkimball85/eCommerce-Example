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
    /// Handles the GET request to display the list of products with pagination.
    /// </summary>
    /// <param name="page">The page number to display (default is 1).</param>
    /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
    public async Task<IActionResult> Index(int page = 1)
    {
        if (page < 1)
            page = 1;

        // Get the total product count
        int totalProducts = await _context.Products.CountAsync();
        int totalPages = (int)Math.Ceiling(totalProducts / (double)PageSize);

        // Retrieve the products for the current page
        List<Product> products = await _context.Products
            .AsNoTracking()
            .OrderBy(p => p.Title)
            .Skip((page - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync();

        // Pass pagination info to the view using ViewData
        ViewData["CurrentPage"] = page;
        ViewData["TotalPages"] = totalPages;
        ViewData["TotalProducts"] = totalProducts;

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