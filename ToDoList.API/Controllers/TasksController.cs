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
public class TasksController : ControllerBase
{
    private ApiDbContext _dbCtx;

    public TasksController(ApiDbContext appDbContext)
    {
        _dbCtx = appDbContext;
    }

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> Get(int id)
    {
        var task = await _dbCtx.ApiTasks
            .FirstOrDefaultAsync(t=>t.Id==id);

        if (task == null)
            return BadRequest("Invalid Id");

        return Ok(task);
    }
    
    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> Create(TaskDto entityDto)
    {
        var entity = new TaskEntity()
        {
            Title = entityDto.Title,
            Description = entityDto.Description,
            ParrentBoxId = entityDto.ParrentBoxId,
        };
        await _dbCtx.ApiTasks.AddAsync(entity);
        await _dbCtx.SaveChangesAsync();

        return CreatedAtAction("Get", new { entity.Id }, entity);
    }

    [HttpPatch]
    [Route("Update")]
    public async Task<IActionResult> Update(int id, TaskDto entityDto)
    {
        var task = await _dbCtx.ApiTasks.FirstOrDefaultAsync(t=>t.Id == id);
        if (task == null)
            return BadRequest();

        task.ParrentBoxId = entityDto.ParrentBoxId;
        task.Title = entityDto.Title;
        task.Description = entityDto.Description;

        await _dbCtx.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> Delete(int id)
    {
        var task = await _dbCtx.ApiTasks.FirstOrDefaultAsync(t => t.Id == id);
        if (task == null)
            return BadRequest();

        _dbCtx.ApiTasks.Remove(task);
        await _dbCtx.SaveChangesAsync();
        return NoContent();
    }
    
}
