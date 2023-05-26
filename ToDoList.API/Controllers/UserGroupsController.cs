using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using ToDoList.API.DAL;
using ToDoList.API.Domain.Dto;
using ToDoList.API.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace ToDoList.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class GroupsController : ControllerBase
{
    private ApiDbContext _dbCtx;

    public GroupsController(ApiDbContext appDbContext)
    {
        _dbCtx = appDbContext;
    }


    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> Get(int id)
    {
        var group = await _dbCtx.ApiGroups
            .Include(u => u.MembersInGroup)
            .Include(u => u.AcessibleBoxes)
            .FirstOrDefaultAsync(g => g.Id == id);
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
        await _dbCtx.ApiGroups.AddAsync(entity);
        await _dbCtx.SaveChangesAsync();
        return CreatedAtAction("Get", new { entity.Id }, entity);
    }


    [HttpPatch]
    [Route("Update")]
    public async Task<IActionResult> Update(int id, UserGroupDto entityDto)
    {
        var group = await _dbCtx.ApiGroups.FirstOrDefaultAsync(g => g.Id == id);
        if (group == null)
            return BadRequest();

        group.Title = entityDto.Title;
        group.Description = entityDto.Description;

        await _dbCtx.SaveChangesAsync();
        return NoContent();
    }


    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = _dbCtx.ApiGroups.FirstOrDefault(g => g.Id == id);
        if (entity == null)
            return BadRequest("Invalid Id");

        _dbCtx.ApiGroups.Remove(entity);
        await _dbCtx.SaveChangesAsync();
        return NoContent();
    }
}
