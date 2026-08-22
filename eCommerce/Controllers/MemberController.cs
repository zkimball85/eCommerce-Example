using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers;

public class MemberController : Controller
{
    public IActionResult Register()
    {
        return View();
    }
}
