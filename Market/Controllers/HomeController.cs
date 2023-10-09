using System.Diagnostics;
using Market.DAL.Interfaces;
using Market.Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Market.Models;

namespace Market.Controllers;

public class HomeController : Controller
{
    public async Task<IActionResult> Index()
    {
        
        return View();
    }
    
    public IActionResult Privacy()
    {
        return View();
    }
}