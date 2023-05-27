using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoList.API.Domain.AccountDto;
using ToDoList.API.Domain.Entities;
using ToDoList.API.Domain.Roles;

namespace ToDoList.API.Services.Auth;

public class AuthService : IAuthService
{
    private RoleManager<IdentityRole> _roleManager;
    private UserManager<UserEntity> _userManager;
    private IConfiguration _config;

    public AuthService(
        UserManager<UserEntity> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration config)
    {
        _roleManager = roleManager;
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
        var newUser = new UserEntity
        {
            Nickname = RegisterDto.Nickname,
            UserName = RegisterDto.Username,
            Email = RegisterDto.Email,
        };

        var result = await _userManager.CreateAsync(newUser, RegisterDto.Password);
        await _userManager.AddToRoleAsync(newUser, ApiUserRoles.User);

        return result.Succeeded;
    }

    public async Task<string> GenerateTokenStringAsync(LoginDto loginDto)
    {
        var user = await _userManager.FindByNameAsync(loginDto.Username);
        var claims = await GetAllValidClaims(user);

        SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));

        SigningCredentials signingCredential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

        var securityToken = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            issuer: _config.GetSection("Jwt:Issuer").Value,
            audience: _config.GetSection("Jwt:Audience").Value,
            signingCredentials: signingCredential);
        var tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
        return tokenString;
    }

    private async Task<List<Claim>> GetAllValidClaims(UserEntity user)
    {
        var _options = new IdentityOptions();

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.Nickname),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var userClaims = await _userManager.GetClaimsAsync(user);
        claims.AddRange(userClaims);


        //add all claims attached to role
        var userRoles = await _userManager.GetRolesAsync(user);
        foreach (var userRole in userRoles)
        {
            var role = await _roleManager.FindByNameAsync(userRole);
            if (role != null)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));

                var roleClaims = await _roleManager.GetClaimsAsync(role);
                foreach (var roleClaim in roleClaims)
                {
                    claims.Add(roleClaim);
                }
            }
        }
        
        return claims;
    }
}
