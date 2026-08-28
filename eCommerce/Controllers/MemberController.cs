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
    /// Handles the registration process for a new member. 
    /// Validates the input model, checks for existing usernames
    /// and emails in the database, and adds the new member if valid.
    /// </summary>
    /// <param name="reg">The registration view model.</param>
    /// <returns>A redirect to the Home page.</returns>
    [HttpPost]
    public async Task<IActionResult> Register(RegistrationViewModel reg)
    {
        if (ModelState.IsValid)
        {

            // Check if the username or email already exists in the database
            bool usernameExists = await _context.Members.AnyAsync(m => m.Username == reg.Username);
            bool emailExists = await _context.Members.AnyAsync(m => m.Email == reg.Email);

            // If either the username or email already exists, add a model error and return the view with the registration model
            if (usernameExists)
            {
                ModelState.AddModelError(nameof(Member.Username), "This username is already taken.");
            }

            // If the email already exists, add a model error and return the view with the registration model
            if (emailExists)
            {
                ModelState.AddModelError(nameof(Member.Email), "This email is already registered.");
            }

            if (usernameExists || emailExists)
            {
                return View(reg);
            }

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
    /// checks for matching username/email and password in the database,
    /// and sets session variables if successful.
    /// </summary>
    /// <param name="login">The login view model.</param>
    /// <returns>A redirect to the Home page if the login is successful,
    /// otherwise returns the login view with validation errors.</returns>
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel login)


    {
        if (ModelState.IsValid)
        {
            // Check if the UsernameOrEmail and Password matches a record in the database
            var loggedInMember = await _context.Members
                                    .Where(m => (m.Username == login.UsernameOrEmail || m.Email == login.UsernameOrEmail) 
                                        && m.Password == login.Password)
                                    .Select(m => new { m.MemberId, m.Username })
                                    .SingleOrDefaultAsync();

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
