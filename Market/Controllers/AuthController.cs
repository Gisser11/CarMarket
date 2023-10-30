using Market.DAL.Interfaces;
using Market.DAL.Repositories.Services;
using Market.Domain.Entity;
using Market.Domain.ViewModels.User;

using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers;

[Route("api")]
public class AuthController : Controller
{
    private readonly IUserRepository _userRepository;
    private readonly JwtService _jwtService;

    public AuthController(IUserRepository userRepository, JwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    [Route("register")]
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] UserRegisterViewModel dto)
    {
        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };

        return Created("Success", await _userRepository.Create(user));
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] UserLoginViewModel loginDto)
    {
        var response = _userRepository.GetByEmail(loginDto.Email);

        if (response == null)
        {
            return BadRequest(new {message="Wrong Data"});
        }

        if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, response.Password))
        {
            return BadRequest(new {message="Wrong Data"});
        }

        var token = _jwtService.GenerateJwtToken(response.Id);
        
        Response.Cookies.Append("token", token, new CookieOptions
        {
            HttpOnly = true
        });
        
        return Ok(new
        {
            message="Ok" 
        });
    }
    
    [Route("list")]
    [HttpGet]
    public async Task<IActionResult> SelectAll()
    {
        var response = await _userRepository.SelectUsers();
        return Ok(response);
    }

    [HttpGet("user")]
    public IActionResult User()
    {
        try
        {
            var cookiesToken = Request.Cookies["token"];

            var token = _jwtService.Verify(cookiesToken);

            int userId = int.Parse(token.Issuer);

            var user = _userRepository.GetById(userId);

            return Ok(user);
        }
        catch (Exception ex)
        {
            return Unauthorized();
        }
    }
    [HttpPost("delete")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("token");

        return Ok(new
        {   
            message = "success"
        });
    }
}