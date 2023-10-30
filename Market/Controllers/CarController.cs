using Market.DAL.Interfaces;
using Market.Domain.Entity;
using Market.Domain.ViewModels.Car;
using Market.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers
{
    [Route("api/cars")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }
        [Route("getcars")]
        [HttpGet]
        public async Task<IActionResult> GetCars()
        {
            var response = await _carService.GetCars();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return Ok(response.Data);
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCar(int id)
        {
            var response = await _carService.GetCar(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return Ok(response.Data);
            }

            return NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _carService.DeleteCar(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return NoContent();
            }

            return NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateCar(CarViewModel carViewModel)
        {
            if (ModelState.IsValid)
            {
                if (carViewModel.Id == 0)
                {
                    await _carService.CreateCar(carViewModel);
                    return CreatedAtAction("GetCar", new { id = carViewModel.Id }, carViewModel);
                }
                else
                {
                    var response = await _carService.Edit(carViewModel.Id, carViewModel);
                    if (response.StatusCode == Domain.Enum.StatusCode.OK)
                    {
                        return NoContent();
                    }
                }
            }

            return BadRequest(ModelState);
        }
    }
}
