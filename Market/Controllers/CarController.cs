using Market.DAL.Interfaces;
using Market.Domain.Entity;
using Market.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers;

public class CarController : Controller
{
    private readonly ICarService _carService;

    public CarController(ICarService carService)
    {
        _carService = carService;
    }

    [HttpGet]
    public async Task<JsonResult> GetCars()
    {
        var response = await _carService.GetCars();
        return Json(Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(response));
    }

    /*public async Task<IActionResult> GetCar(int id)
    {
        var response = await _carService.GetCar(id);

        return View(response);
    }*/
}