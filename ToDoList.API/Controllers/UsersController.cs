using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.API.DAL;
using ToDoList.API.Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ToDoList.API.Services.Check;
using ToDoList.API.Domain.Entities;
using ToDoList.API.Domain.Roles;

namespace ToDoList.API.Controllers;


[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private ApiDbContext _dbCtx;
    private UserManager<UserEntity> _userManager;
    private IAcessGuardService _acessCheck;
    private ICheckExistingRecordService _existCheck;

    public UsersController(
        ApiDbContext appDbContext, 
        UserManager<UserEntity> userManager,
        IAcessGuardService acessCheck,
        ICheckExistingRecordService existCheck)
    {
        _dbCtx = appDbContext;
        _userManager = userManager;
        _acessCheck = acessCheck;
        _existCheck = existCheck;
    }

    //Users are created in AuthController

    [HttpGet]
    [Authorize(Roles = "ApiAdmin")]
    [Route("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userManager.Users.ToListAsync();
        return Ok(users);
    }


    [HttpGet]
    [Route("GetSelf")]
    public async Task<IActionResult> GetSelf()
    {
        var userId = _userManager.GetUserId(User);
        var user = await _dbCtx.Users
            .Include(u => u.GroupMemberships)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null) 
            return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected error");

        return Ok(user);
    }



    [HttpPatch]
    [Route("Update")]
    public async Task<IActionResult> Update(string userId, UserDto entityDto)
    {
        var user = await _dbCtx.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
            return BadRequest("Invalid Id");

        if (!(await _acessCheck.IsUserAcessibleAsync(userId)))
            return Unauthorized("Acess denied");


        user.Nickname = entityDto.Nickname;

        await _dbCtx.SaveChangesAsync();
        return NoContent();
    }


    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> Delete(string userId)
    {
        var user = await _dbCtx.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
            return BadRequest("Invalid Id");

        if (!(await _acessCheck.IsUserAcessibleAsync(userId)))
            return Unauthorized("Acess denied");


        await _userManager.DeleteAsync(user);
        await _dbCtx.SaveChangesAsync();
        return NoContent();
    }


    
}
