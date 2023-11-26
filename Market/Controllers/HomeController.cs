using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : Controller
{
    public async Task<IActionResult> Index()
    {
        return Ok();
    }
}