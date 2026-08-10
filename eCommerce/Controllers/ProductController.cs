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
}