using Identity.Application.Services;
using Identity.Domain.DTOs;
using Identity.Domain.Entities;
using Identity.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;

    public UsersController(IAuthService authService, IUserService userService)
    {
        _authService = authService;
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var a = HttpContext.RequestAborted;

        return Ok(_userService.Get());
    }

    [HttpPost("register")]
    public async ValueTask<IActionResult> Register(User user)
    {
        var a = HttpContext.RequestAborted;
        
        return Ok(await _authService.RegisterAsync(user,a));
    }
    [HttpPost("login")]
    public async ValueTask<IActionResult> Login(UserForLogin user)
    {
        var a = HttpContext.RequestAborted;

        return Ok(await _authService.LodinAsync(user, a));
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{userId:guid}/grandRole")]
    public IActionResult Update([FromRoute] Guid userId,Role role)
    {
        var a = HttpContext.RequestAborted;
        return Ok(_authService.GrandRoleAsync(userId,role,a));
    }
    
}
