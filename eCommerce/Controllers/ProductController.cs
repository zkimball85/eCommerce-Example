using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Controllers;

public class ProductController : Controller
{
    private readonly ProductDbContext _context;

    public ProductController(ProductDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // Retrieve the list of products from the database
        List<Product> allProducts = await _context.Products.AsNoTracking().ToListAsync();
        return View(allProducts);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

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

    [HttpGet]

    public async Task<IActionResult> Edit(int id)
    {
        // Retrieve the product from the database
        Product? product = await _context.Products.Where(p => p.ProductId == id).FirstOrDefaultAsync();
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

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
}