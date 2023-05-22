using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.API.DAL;
using ToDoList.API.Domain.Dto;
using ToDoList.API.Domain.Entities;

namespace ToDoList.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class TaskBoxesController : ControllerBase
{
    private AppDbContext _dbCtx;

    public TaskBoxesController(AppDbContext appDbContext)
    {
        _dbCtx = appDbContext;
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var rootBoxes = await _dbCtx.TaskBoxes.ToListAsync();
        return Ok(rootBoxes);
    }

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> Get(int id)
    {
        var box = await _dbCtx.TaskBoxes.FirstOrDefaultAsync(b => b.Id == id);
        if (box == null)
            return BadRequest("Invalid Id");

        return Ok(box);
    }

    
    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> Create(TaskBoxDto entityDto)
    {
        var entity = new TaskBoxEntity()
        {
            AssociatedGroupId = entityDto.AssociatedGroupId,
            ParrentBoxId = entityDto.ParrentBoxId,
            Title = entityDto.Title,
        };
        await _dbCtx.TaskBoxes.AddAsync(entity);
        await _dbCtx.SaveChangesAsync();
        return CreatedAtAction("Get", new { entity.Id }, entity);
    }
    

    [HttpPatch]
    [Route("Update")]
    public async Task<IActionResult> Update(int id, TaskBoxEntity entity)
    {
        var box = await _dbCtx.TaskBoxes.FirstOrDefaultAsync(b => b.Id == id);
        if (box == null)
            return BadRequest();

        box.Id = entity.Id;
        box.Title = entity.Title;
        box.AssociatedGroupId = entity.AssociatedGroupId; 
        box.ParrentBoxId = entity.ParrentBoxId;

        await _dbCtx.SaveChangesAsync();
        return NoContent();
    }
    

    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> Delete(int id)
    {
        var box = await _dbCtx.TaskBoxes.FirstOrDefaultAsync(b => b.Id == id);
        if (box == null)
            return BadRequest();

        _dbCtx.TaskBoxes.Remove(box);
        await _dbCtx.SaveChangesAsync();
        return NoContent();
    }
}
