using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers;

[Route("api/Studia")]
public class StudiaController : Controller
{
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {

        return NotFound();
        
    }
}