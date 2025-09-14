using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    public IActionResult Home()
    {
        return View();
    }
}