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
    private readonly IUserRepository _userRepository;
    public StudiaController(IStudiaService studiaService, JwtService jwtService, IUserRepository userRepository)
    {
        _studiaService = studiaService;
        _jwtService = jwtService;
        _userRepository = userRepository;
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var response = await _studiaService.GetAllStudia(); 
        
        if (response.StatusCode == Domain.Enum.StatusCode.OK) 
            return Ok(response.Data);
        
        return Ok("Не найдено записей");
    }

    [Route("CreateOrUpdate")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] StudiaViewModel studiaViewModel)
    {
        try
        {
            var cookiesToken = Request.Cookies["token"];
            _jwtService.Verify(cookiesToken);
            
            
            if (studiaViewModel.Id == 0)
            {
                await _studiaService.CreateStudia(studiaViewModel);
                return Ok("успешно");
            }
            //TODO EDIT METHOD HERE

            return NoContent();
        }
        catch (Exception ex)
        {
            return Unauthorized();
        }
    }
}