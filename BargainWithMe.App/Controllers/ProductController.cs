using Microsoft.AspNetCore.Mvc;

namespace BargainWithMe.App.Controllers;
public class ProductController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
