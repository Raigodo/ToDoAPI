using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.API.DAL;
using ToDoList.API.Domain.Dto;
using ToDoList.API.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using ToDoList.API.Services.Check;

namespace ToDoList.API.Controllers;


[ApiController]
[Authorize(Roles = "ApiUser,ApiAdmin")]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private ApiDbContext _dbCtx;
    private UserManager<UserEntity> _userManager;
    private IAcessGuardService _acessCheck;
    private ICheckExistingRecordService _existCheck;

    public TasksController(
        ApiDbContext appDbContext,
        UserManager<UserEntity> userManager,
        IAcessGuardService acessCheck,
        ICheckExistingRecordService existCheck)
    {
        _dbCtx = appDbContext;
        _userManager = userManager;
        _acessCheck = acessCheck;
        _existCheck = existCheck;
    }

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> Get(int taskId)
    {
        var task = await _dbCtx.ApiTasks
            .FirstOrDefaultAsync(t => t.Id == taskId);
        if (task == null) 
            return BadRequest("Invalid Id");

        if (!(await _acessCheck.IsTaskAcessibleAsync(taskId)))
            return Unauthorized("Acess denied");


        return Ok(task);
    }
    
    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> Create(TaskDto entityDto)
    {
        if (!(await _existCheck.DoesBoxExistAsync(entityDto.ParrentBoxId)))
            return BadRequest("Invalid Id");

        if (!(await _acessCheck.IsBoxAcessibleAsync(entityDto.ParrentBoxId)))
            return Unauthorized("Acess denied");


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
    public async Task<IActionResult> Update(int taskId, TaskDto entityDto)
    {
        var task = await _dbCtx.ApiTasks
            .Include(t => t.ParrentBox)
            .Include(t => t.ParrentBox.AssociatedGroup)
            .FirstOrDefaultAsync(t => t.Id == taskId);

        if (task == null ||
            !(await _existCheck.DoesBoxExistAsync(entityDto.ParrentBoxId)))
            return BadRequest("Invalid Id");

        if (!(await _acessCheck.IsTaskAcessibleAsync(taskId)) ||
            !(await _acessCheck.IsBoxAcessibleAsync(entityDto.ParrentBoxId)))
            return Unauthorized("Acess denied");


        task.ParrentBoxId = entityDto.ParrentBoxId;
        task.Title = entityDto.Title;
        task.Description = entityDto.Description;

        await _dbCtx.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> Delete(int taskId)
    {
        var task = await _dbCtx.ApiTasks
            .Include(t => t.ParrentBox)
            .Include(t => t.ParrentBox.AssociatedGroup)
            .FirstOrDefaultAsync(t => t.Id == taskId);

        if (task == null)
            return BadRequest("Invalid Id");

        if (await _acessCheck.IsTaskAcessibleAsync(taskId))
            return Unauthorized("Acess denied");


        _dbCtx.ApiTasks.Remove(task);
        await _dbCtx.SaveChangesAsync();
        return NoContent();
    }
}
