using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using ToDoList.API.DAL;
using ToDoList.API.Domain.Dto;
using ToDoList.API.Domain.Entities;

namespace ToDoList.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private AppDbContext _dbCtx;

    public UsersController(AppDbContext appDbContext)
    {
        _dbCtx = appDbContext;
    }

    
    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> Get(int id)
    {
        var user = await _dbCtx.Users
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

        await _dbCtx.Users.AddAsync(entity);
        await _dbCtx.SaveChangesAsync();
        return CreatedAtAction("Get", new { id = entity.Id }, entity);
    }


    [HttpPatch]
    [Route("Update")]
    public async Task<IActionResult> Update(int id, UserDto entityDto)
    {
        var user = await _dbCtx.Users.FirstOrDefaultAsync(u => u.Id == id);
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
        var user = _dbCtx.Users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return BadRequest("Invalid Id");

        _dbCtx.Users.Remove(user);
        await _dbCtx.SaveChangesAsync();
        return NoContent();
    }
}
