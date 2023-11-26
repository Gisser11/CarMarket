using Market.DAL.Interfaces;
using Market.DAL.Repositories.Services;
using Market.Domain.Entity;
using Market.Domain.Enum.UserRoles;
using Market.Domain.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers;

[Route("[controller]")]
public class AdminController : Controller
{
    private readonly IUserRepository _userRepository;
    

    public AdminController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    [Route("StudiaPage")]
    public async Task<IActionResult> StudiaPage()
    {
        return View();
    }
    
    [HttpDelete]
    [Route("DeleteUserId/{Id:int}")]
    public IActionResult DeleteUser([FromRoute] int Id)
    {
        var response = _userRepository.Delete(Id);
        return Ok(Id);
    }

    [HttpPost]
    [Route("CreateOrUpdate")]
    public async Task<IActionResult> Create([FromBody] UserViewModel dto)
    {
        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            TypeUserRole = "Moderator"
        };
        
        return Created("Success", await _userRepository.Create(user));
    }
    
}