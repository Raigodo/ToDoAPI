using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToDoList.API.Domain.AccountDto;
using ToDoList.API.Services;

namespace ToDoList.API.Controllers;


[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var result = await _authService.Login(loginDto);

        if (!result)
            return BadRequest("Incorrect Username or Password");

        var tokenString = _authService.GenerateTokenString(loginDto);
        return Ok(tokenString);
    }

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        var result = await _authService.Register(registerDto);

        if (!result)
            return BadRequest("registration failed");

        return Ok("sucess");
    }
}
