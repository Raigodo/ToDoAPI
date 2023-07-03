using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoList.Application.Dto.Receive.User;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Roles;

namespace ToDoList.Services.Auth;

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

    public async Task<bool> Login(ReceiveLoginDto loginDto)
    {
        var user = await _userManager.FindByNameAsync(loginDto.Username);
        if (user == null)
            return false;

        return await _userManager.CheckPasswordAsync(user, loginDto.Password);
    }

    public async Task<bool> Register(ReceiveRegisterDto registerDto)
    {
        var newUser = new UserEntity
        {
            Nickname = registerDto.Nickname,
            UserName = registerDto.Username,
            Email = registerDto.Email,
        };

        var result = await _userManager.CreateAsync(newUser, registerDto.Password);
        await _userManager.AddToRoleAsync(newUser, ApiUserRoles.User);

        return result.Succeeded;
    }

    public async Task<string> GenerateTokenStringAsync(ReceiveLoginDto loginDto)
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
