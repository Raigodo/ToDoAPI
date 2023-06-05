using Microsoft.AspNetCore.Mvc;
using ToDoList.API.Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using ToDoList.API.Services.Check;
using ToDoList.API.DAL.Interfaces;
using ToDoList.API.Domain.Roles;

namespace ToDoList.API.Controllers;


//NOTICE Users are created in AuthController
[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private IAcessGuardService _acessCheck;
    private IUserRepository _userRepository;

    public UsersController(
        IAcessGuardService acessCheck,
        IUserRepository userRepository)
    {
        _acessCheck = acessCheck;
        _userRepository = userRepository;
    }


    [HttpGet]
    [Authorize(Roles = ApiUserRoles.Admin)]
    [Route("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userRepository.GetAllUsersAsync();
        return Ok(users);
    }


    [HttpGet]
    [Route("GetSelf")]
    public async Task<IActionResult> GetSelf()
    {
        var user = await _userRepository.GetCurrentUserAsync();

        if (user == null) 
            return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected error");

        return Ok(user);
    }


    [HttpGet]
    [Route("ViewUser")]
    public async Task<IActionResult> ViewUser(string userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);

        if (user == null)
            return NotFound("User not found");

        return Ok(new { 
            user.Nickname,
            user.GroupMemberships
        });
    }


    [HttpPatch]
    [Route("Update")]
    public async Task<IActionResult> Update(string userId, UserDto entityDto)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);

        if (user == null)
            return BadRequest("user not found");

        if (!(await _acessCheck.IsUserAcessibleAsync(userId)))
            return Unauthorized("Acess denied");


        var result = await _userRepository.UpdateUserAsync(user, entityDto);
        return NoContent();
    }


    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> Delete(string userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);

        if (user == null)
            return BadRequest("User not found");

        if (!(await _acessCheck.IsUserAcessibleAsync(userId)))
            return Unauthorized("Acess denied");

        await _userRepository.DeleteUserAsync(user);
        return NoContent();
    }
}
