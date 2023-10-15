using System.Diagnostics;
using Market.DAL.Interfaces;
using Market.Domain.Entity;
using Microsoft.AspNetCore.Mvc;


namespace Market.Controllers;

public class HomeController : Controller
{
    public async Task<IActionResult> Index()
    {
        
        return Ok();
    }
}