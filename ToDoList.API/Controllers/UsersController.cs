using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.API.DAL;
using ToDoList.API.Domain.Dto;
using ToDoList.API.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace ToDoList.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private ApiDbContext _dbCtx;

    public UsersController(ApiDbContext appDbContext)
    {
        _dbCtx = appDbContext;
    }

    
    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> Get(int id)
    {
        var user = await _dbCtx.ApiUsers
            .Include(u=>u.MemberingGroups)
            .FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
            return BadRequest("Invalid Id");

        return Ok(user);
    }


    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> Create(UserDto entityDto)
    {
        var entity = new UserEntity()
        {
            Nickname = entityDto.Nickname,
        };

        await _dbCtx.ApiUsers.AddAsync(entity);
        await _dbCtx.SaveChangesAsync();
        return CreatedAtAction("Get", new { id = entity.Id }, entity);
    }


    [HttpPatch]
    [Route("Update")]
    public async Task<IActionResult> Update(int id, UserDto entityDto)
    {
        var user = await _dbCtx.ApiUsers.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
            return BadRequest();

        user.Nickname = entityDto.Nickname;

        await _dbCtx.SaveChangesAsync();
        return NoContent();
    }


    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = _dbCtx.ApiUsers.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return BadRequest("Invalid Id");

        _dbCtx.ApiUsers.Remove(user);
        await _dbCtx.SaveChangesAsync();
        return NoContent();
    }
}
