using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.API.DAL;
using ToDoList.API.Domain.Dto;
using ToDoList.API.Domain.Entities;

namespace ToDoList.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private AppDbContext _dbCtx;

    public TasksController(AppDbContext appDbContext)
    {
        _dbCtx = appDbContext;
    }

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> Get(int id)
    {
        var task = await _dbCtx.ToDoTasks.FirstOrDefaultAsync(t=>t.Id==id);

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
        await _dbCtx.ToDoTasks.AddAsync(entity);
        await _dbCtx.SaveChangesAsync();

        return CreatedAtAction("Get", new { entity.Id }, entity);
    }

    [HttpPatch]
    [Route("Update")]
    public async Task<IActionResult> Update(int id, TaskEntity entity)
    {
        var task = await _dbCtx.ToDoTasks.FirstOrDefaultAsync(t=>t.Id == id);
        if (task == null)
            return BadRequest();

        task.Id = entity.Id;
        task.ParrentBoxId = entity.ParrentBoxId;
        task.Title = entity.Title;
        task.Description = entity.Description;

        await _dbCtx.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> Delete(int id)
    {
        var task = await _dbCtx.ToDoTasks.FirstOrDefaultAsync(t => t.Id == id);
        if (task == null)
            return BadRequest();

        _dbCtx.ToDoTasks.Remove(task);
        await _dbCtx.SaveChangesAsync();
        return NoContent();
    }
    
}
