using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Dto.Receive.User;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Roles;

namespace ToDoList.API.Controllers;

[Route("api/[controller]")]
[Authorize(Roles = ApiUserRoles.Admin)]
[ApiController]
public class RolesController : ControllerBase
{
    private UserManager<UserEntity> _userManager;
    private RoleManager<IdentityRole> _roleManager;

    public RolesController(
        UserManager<UserEntity> userManager,
        RoleManager<IdentityRole> roleManager)
    {
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
    public async Task<IActionResult> AddRole(string roleName)
    {
        var doesRoleExist = await _roleManager.RoleExistsAsync(roleName);

        if (doesRoleExist)
            return BadRequest("Role already exist");

        var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));

        if (roleResult.Succeeded)
            return CreatedAtAction("GetAllRoles", null, roleName);

        return BadRequest("Something went wrong");
    }

    [HttpPost]
    [Route("AddUserToRole")]
    public async Task<IActionResult> AddUserToRole(ReceiveUserIdDto userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId.Id);

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
    public async Task<IActionResult> GetUserRoles(ReceiveUserIdDto userId)
    {
        var user = await _userManager.FindByIdAsync(userId.Id);

        if (user == null)
            return BadRequest("User does not exist");

        var roles = await _userManager.GetRolesAsync(user);

        return Ok(roles);
    }

    [HttpGet]
    [Route("RemoveUserFromRole")]
    public async Task<IActionResult> RemoveUserFromRole(ReceiveUserIdDto userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId.Id);

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
