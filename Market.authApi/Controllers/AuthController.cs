using Microsoft.AspNetCore.Mvc;
using Market.DAL.Interfaces;

namespace Market.authApi.Controllers;

public class AuthController : Controller
{
    private readonly IUserRepositry _userRepositry;
}