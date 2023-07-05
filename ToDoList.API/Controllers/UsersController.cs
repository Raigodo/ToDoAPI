using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Dto.Receive.User;
using ToDoList.Application.Exceptions;
using ToDoList.Application.Services.Users;

namespace ToDoList.API.Controllers;


//NOTICE Users are created in AuthController
[ApiController]
//[Authorize]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private UsersService _userService;

    public UsersController(
        UsersService userService)
    {
        _userService = userService;
    }


    [HttpGet]
    //[Authorize(Roles = ApiUserRoles.Admin)]
    [Route("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllAsync();
        return Ok(users);
    }


    [HttpGet]
    [Route("Get/{userId}")]
    public async Task<IActionResult> Get(string userId)
    {
        return Ok();
    }

    [HttpGet]
    [Route("Inspect/{userId}")]
    public async Task<IActionResult> Inspect(string userId)
    {
        return Ok();
    }

    [HttpGet]
    [Route("Get/{userId}/MemberingGroups")]
    public async Task<IActionResult> MemberingGroups(string userId)
    {
        return Ok();
    }



    [HttpPatch]
    [Route("Update")]
    public async Task<IActionResult> Update(ReceiveUpdateUserDto userDto)
    {
        return Ok();
    }


    [HttpDelete]
    [Route("Delete/{userId}")]
    public async Task<IActionResult> Delete(string userId)
    {
        return Ok();
    }
}
