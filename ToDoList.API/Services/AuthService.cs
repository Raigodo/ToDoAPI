using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoList.API.Domain.AccountDto;
using ToDoList.API.Domain.Entities;

namespace ToDoList.API.Services;

public class AuthService : IAuthService
{
    private UserManager<UserEntity> _userManager;
    private IConfiguration _config;

    public AuthService(UserManager<UserEntity> userManager, IConfiguration config)
    {
        _userManager = userManager;
        _config = config;
    }

    public async Task<bool> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByNameAsync(loginDto.Username);
        if (user == null)
            return false;

        return await _userManager.CheckPasswordAsync(user, loginDto.Password);
    }

    public async Task<bool> Register(RegisterDto RegisterDto)
    {
        var user = new UserEntity
        {
            UserName = RegisterDto.Username,
            Email = RegisterDto.Email,
        };

        var result = await _userManager.CreateAsync(user, RegisterDto.Password);


        return result.Succeeded;
    }

    public async Task<string> GenerateTokenStringAsync(LoginDto loginDto)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, (await _userManager.FindByNameAsync(loginDto.Username)).Id),
            new Claim(ClaimTypes.Name, loginDto.Username),
            new Claim(ClaimTypes.Role, "Admin")
        };

        SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));

        SigningCredentials signingCredential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

        var securityToken = new JwtSecurityToken(
            claims:claims,
            expires: DateTime.Now.AddMinutes(60),
            issuer: _config.GetSection("Jwt:Issuer").Value,
            audience: _config.GetSection("Jwt:Audience").Value,
            signingCredentials: signingCredential);
        var tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
        return tokenString; 
    }
}
