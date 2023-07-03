using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Dto.Receive.User;
using ToDoList.Application.Interfaces;
using ToDoList.Domain.Roles;

namespace ToDoList.API.Controllers;


//NOTICE Users are created in AuthController
[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private IUserRepository _userRepository;

    public UsersController(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }


    [HttpGet]
    //[Authorize(Roles = ApiUserRoles.Admin)]
    [Route("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userRepository.GetAllUsersAsync();
        return Ok(users);
    }


    [HttpGet]
    [Route("Inspect")]
    public async Task<IActionResult> Inspect(ReceiveUserIdDto userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);

        return Ok(user);
    }


    [HttpPatch]
    [Route("Update")]
    public async Task<IActionResult> Update(ReceiveUpdateUserDto userDto)
    {
        await _userRepository.UpdateUserAsync(userDto);
        return NoContent();
    }


    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> Delete(ReceiveUserIdDto userId)
    {
        await _userRepository.DeleteUserAsync(userId);
        return NoContent();
    }
}
