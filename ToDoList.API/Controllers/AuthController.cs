using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Dto.Receive.User;
using ToDoList.Services.Auth;

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
    public async Task<IActionResult> Login(ReceiveLoginDto login)
    {
        var result = await _authService.Login(login);

        if (!result)
            return BadRequest("Incorrect Username or Password");

        var tokenString = await _authService.GenerateTokenStringAsync(login);
        return Ok(tokenString);
    }

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register(ReceiveRegisterDto register)
    {
        var result = await _authService.Register(register);

        if (!result)
            return BadRequest("Registration failed");

        return Ok("User registered sucesfully");
    }
}
