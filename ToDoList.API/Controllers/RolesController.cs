using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Xml.Linq;
using ToDoList.API.DAL;
using ToDoList.API.Domain.Entities;

namespace ToDoList.API.Controllers;

[Route("api/[controller]")]
[Authorize(Roles = "ApiAdmin")]
[ApiController]
public class RolesController : ControllerBase
{
    private ApiDbContext _dbCtx;
    private UserManager<UserEntity> _userManager;
    private RoleManager<IdentityRole> _roleManager;

    public RolesController(
        ApiDbContext dbCtx,
        UserManager<UserEntity> userManager,
        RoleManager<IdentityRole> roleManager
        )
    {
        _dbCtx = dbCtx;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpGet]
    [Route("GetAllRoles")]
    public async Task<IActionResult> GetAllRoles()
    {
        var roles = await _roleManager.Roles.ToListAsync();
        return Ok(roles);
    }


    [HttpPost]
    [Route("AddRole")]
    public async Task<IActionResult> AddRole(string name)
    {
        var doesRoleExist = await _roleManager.RoleExistsAsync(name);

        if (doesRoleExist)
            return BadRequest("Role already exist");

        var roleResult = await _roleManager.CreateAsync(new IdentityRole(name));

        if (roleResult.Succeeded)
        {
            return CreatedAtAction("GetAllRoles", new { }, name);
        }
        
        return BadRequest("Something went wrong");
    }

    [HttpPost]
    [Route("AddUserToRole")]
    public async Task<IActionResult> AddUserToRole(string userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return BadRequest("User does not exist");

        var doesRoleExist = await _roleManager.RoleExistsAsync(roleName);

        if (doesRoleExist)
            return BadRequest($"Role {roleName} does not exist");

        var result = await _userManager.AddToRoleAsync(user, roleName);

        if (result.Succeeded) 
            return CreatedAtAction("GetUserRoles", new { userId }, roleName);

        return BadRequest("Something went wrong");
    }

    [HttpGet]
    [Route("GetUserRoles")]
    public async Task<IActionResult> GetUserRoles(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return BadRequest("User does not exist");

        var roles = await _userManager.GetRolesAsync(user);

        return Ok(roles);
    }

    [HttpGet]
    [Route("RemoveUserFromRole")]
    public async Task<IActionResult> RemoveUserFromRole(string userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return BadRequest("User does not exist");

        if (!(await _roleManager.RoleExistsAsync(roleName)))
            return BadRequest("Role does not exist");

        var result = await _userManager.RemoveFromRoleAsync(user, roleName);

        if (result.Succeeded)
            return Ok($"User removed from {roleName} role sucessfuly");

        return BadRequest("Something went wrong");
    }
}
