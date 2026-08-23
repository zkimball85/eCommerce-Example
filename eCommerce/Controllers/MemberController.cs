using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace eCommerce.Controllers;

public class MemberController : Controller
{

    private readonly ProductDbContext _context;

    public MemberController(ProductDbContext context)
    {
        _context = context;
    }

    public IActionResult Register()
    {
        return View();
    }

    /// <summary>
    /// Handles the registration of a new member. 
    /// Validates the input model, maps it to the Member entity,
    /// and saves it to the database. Redirects to the Home page
    /// upon successful registration.
    /// </summary>
    /// <param name="reg">The registration view model.</param>
    /// <returns>A redirect to the Home page.</returns>
    [HttpPost]
    public async Task<IActionResult> Register(RegistrationViewModel reg)
    {
        if (ModelState.IsValid)
        {

            // Map the RegistrationViewModel to the Member entity
            Member newMember = new()
            {
                Username = reg.Username,
                Email = reg.Email,
                Password = reg.Password,
                DateOfBirth = reg.DateOfBirth
            };

            // Add the new member to the database
            _context.Members.Add(newMember);
            await _context.SaveChangesAsync();
            // Redirect to Home Page
            return RedirectToAction("Index", "Home");
        }

        return View(reg);
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    /// <summary>
    /// Handles the login process for a member. Validates the input model,
    /// checks the credentials against the database, and logs the user in if valid.
    /// </summary>
    /// <param name="login">The login view model.</param>
    /// <returns>A redirect to the Home page if the login is successful, otherwise returns the login view with validation errors.</returns>
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel login)


    {
        if (ModelState.IsValid)
        {
            // Check if the UsernameOrEmail and Password matches a record in the database
            Member? loggedInMember = await _context.Members.Where(m => (m.Username == login.UsernameOrEmail || m.Email == login.UsernameOrEmail) && m.Password == login.Password).SingleOrDefaultAsync();

            if (loggedInMember == null)
            {
                ModelState.AddModelError(string.Empty, "The provided username or email and password do not match any existing account.");
                return View(login);
            }

            // Log the user in?
            HttpContext.Session.SetString("Username", loggedInMember.Username);
            HttpContext.Session.SetInt32("MemberId", loggedInMember.MemberId);

            return RedirectToAction("Index", "Home");
        }

        return View(login);
    }

    public IActionResult Logout()
    {
        // Clear the session to log the user out
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}
