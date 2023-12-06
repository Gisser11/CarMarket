using Market.DAL.Interfaces;
using Market.DAL.Repositories.Services;
using Market.Domain.Entity;
using Market.Domain.ViewModels.User;
using Market.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers;

[Route("[controller]")]
public class AuthController : Controller
{
    #region SERVICES INIT
    
    private readonly IUserService _userService;
    private readonly IUserRepository _userRepository;

    public AuthController(IUserRepository userRepository, IUserService userService)
    {
        _userRepository = userRepository;
        _userService = userService;
    }
    

    #endregion

    #region UserAuthorize

    [Route("checkEmail")]
    [HttpPost]
    public async Task<IActionResult> checkEmail([FromBody] UserCheckEmailViewModel userCheckEmailViewModel)
    {
        var response = _userService.CheckUserEmail(userCheckEmailViewModel.Email);
        
        if (response.Result.StatusCode != Domain.Enum.StatusCode.OK)
        {
            return Ok(false);
        }
        
        return Ok(true);
    }
    
    [Route("GetById/{id:int}")]
    [HttpGet]
    public async Task<IActionResult> Register([FromRoute] int id)
    {
        var response = _userService.GetById(id);

        return Ok(response.Result);
    }
    
    [Route("register")]
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] UserRegisterViewModel dto)
    {
        dto.TypeUserRole = false;
        
        var response = _userService.RegisterUser(dto);
        
        return Json(response);
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] UserLoginViewModel loginDto)
    {
        var response = _userService.LoginUser(loginDto);

        if (response.Result.StatusCode == Domain.Enum.StatusCode.OK)
        {
            var token = response.Result.Token;
            Response.Cookies.Append("token", token);
            return Json(response);
        }

        var ex = response.Result;
        return BadRequest(ex);
    }
    
    // СПИСОК ВСЕХ USERS
    [Route("GetAll")]
    [HttpGet]
    public async Task<IActionResult> SelectAll()
    {
        var response = await _userService.SelectAll();
        return Json(response);
    }
    
    [HttpGet("user")]
    public IActionResult User()
    {
        var cookiesToken = Request.Cookies["token"];
        
        if (cookiesToken == null)
        {
            return Unauthorized();
        } 
        
        var response = _userService.GetUser(cookiesToken);

        return Json(response);
    }
    
    // ВЫХОД ИЗ СИСТЕМЫ
    [HttpPost("delete")]
    public IActionResult Logout()
    {
        try
        {
            Response.Cookies.Delete("token");
            return Json("Success");
        }
        catch (Exception ex)
        {
            return Json(ex);
        }

    }

    #endregion
}