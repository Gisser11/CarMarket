using Market.DAL.Interfaces;
using Market.DAL.Repositories.Services;
using Market.Domain.ViewModels.StudiaViewModel;
using Market.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers;

[ApiController]
[Route("api/Studia")]
public class StudiaController : Controller
{
    private readonly JwtService _jwtService;
    private readonly IStudiaService _studiaService;
    
    public StudiaController(IStudiaService studiaService, JwtService jwtService)
    {
        _studiaService = studiaService;
        _jwtService = jwtService;
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var response = await _studiaService.GetAllStudia(); 
        
        if (response.StatusCode == Domain.Enum.StatusCode.OK) 
            return Ok(response.Data);
        
        return Ok("Не найдено записей");
    }
}