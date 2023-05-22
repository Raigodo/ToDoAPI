using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.API.DAL;
using ToDoList.API.Domain.Dto;
using ToDoList.API.Domain.Entities;

namespace ToDoList.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserGroupsController : ControllerBase
{
    private AppDbContext _dbCtx;

    public UserGroupsController(AppDbContext appDbContext)
    {
        _dbCtx = appDbContext;
    }


    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> Get(int id)
    {
        var group = await _dbCtx.Groups.FirstOrDefaultAsync(g => g.Id == id);
        if (group == null)
            return BadRequest("Invalid Id");

        return Ok(group);
    }


    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> Create(UserGroupDto entityDto)
    {
        var entity = new UserGroupEntity()
        {
            Title = entityDto.Title,
            Description = entityDto.Description,
        };
        await _dbCtx.Groups.AddAsync(entity);
        await _dbCtx.SaveChangesAsync();
        return CreatedAtAction("Get", new { entity.Id }, entity);
    }


    [HttpPatch]
    [Route("Update")]
    public async Task<IActionResult> Update(int id, UserGroupEntity entity)
    {
        var group = await _dbCtx.Groups.FirstOrDefaultAsync(g => g.Id == id);
        if (group == null)
            return BadRequest();

        group.Id = entity.Id;
        group.Title = entity.Title;
        group.Description = entity.Description;

        await _dbCtx.SaveChangesAsync();
        return NoContent();
    }


    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = _dbCtx.Groups.FirstOrDefault(g => g.Id == id);
        if (entity == null)
            return BadRequest("Invalid Id");

        _dbCtx.Groups.Remove(entity);
        await _dbCtx.SaveChangesAsync();
        return NoContent();
    }
}
