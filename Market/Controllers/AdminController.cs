using Market.DAL.Interfaces;
using Market.DAL.Repositories.Services;
using Market.Domain.Entity;
using Market.Domain.Enum.UserRoles;
using Market.Domain.Response;
using Market.Domain.ViewModels.StudiaViewModel;
using Market.Domain.ViewModels.User;
using Market.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers;

[Route("[controller]")]
public class AdminController : Controller
{
    private readonly IUserRepository _userRepository;
    private readonly IAdminService _adminService;
    private readonly IStudiaService _studiaService;
    private readonly IAssortmentService _assortmentService;

    public AdminController(IUserRepository userRepository, IAdminService adminService, IStudiaService studiaService, IAssortmentService assortmentService)
    {
        _userRepository = userRepository;
        _adminService = adminService;
        _studiaService = studiaService;
        _assortmentService = assortmentService;
    }
    
    [Route("StudiaPage")]
    public IActionResult StudiaPage()
    {
        return View();
    }
    
        [Route("UserPage")]
    public IActionResult UserPage()
    {
        return View();
    }

    #region STUDIA CRUD
    [Route("CreateOrUpdateStudia")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] StudiaViewModel studiaViewModel)
    {
        try
        {
            // var cookiesToken = Request.Cookies["token"];
            // _jwtService.Verify(cookiesToken);
            
            
            // if (studiaViewModel.Id == 0)
            // {
            //     await _studiaService.CreateStudia(studiaViewModel);
            //     return Ok("успешно");
            // }

            await _adminService.EditStudia(studiaViewModel.Id, studiaViewModel);

            return NoContent();
        }
        catch (Exception ex)
        {
            return Unauthorized();
        }
    }

    [HttpGet]
    [Route("GetAssortment/{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var response = await _assortmentService.GetAssortmentList(id);
        
        return Ok(response.Data);
    }

    #endregion
    
    
    #region USER CRUD
    [HttpDelete]
    [Route("DeleteUserId/{Id:int}")]
    public IActionResult DeleteUser([FromRoute] int Id)
    {
        var response = _userRepository.Delete(Id);
        return Ok(Id);
    }
    
    [HttpPost]
    [Route("CreateOrUpdateUser")]
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

    
    [HttpPut]
    [Route("EditUserName")]
    public IActionResult EditNode([FromBody] UserViewModel dto)
    {
        var response = _adminService.Edit(dto.Id, dto);
        return Ok(response);
    }
    #endregion
    
}