using Microsoft.AspNetCore.Mvc;
using ToDoList.API.DAL;
using ToDoList.API.Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using ToDoList.API.Services.Check;
using ToDoList.API.DAL.Interfaces;

namespace ToDoList.API.Controllers;


[ApiController]
[Authorize]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private IAcessGuardService _acessCheck;
    private ICheckExistingRecordService _existCheck;
    private ITaskRepository _taskRepository;

    public TasksController(
        IAcessGuardService acessCheck,
        ICheckExistingRecordService existCheck,
        ITaskRepository taskRepository)
    {
        _acessCheck = acessCheck;
        _existCheck = existCheck;
        _taskRepository = taskRepository;
    }

    [HttpGet]
    [Route("ViewTask")]
    public async Task<IActionResult> ViewTask(int taskId)
    {
        var task = _taskRepository.GetTaskByIdAsync(taskId);
        if (task == null) 
            return BadRequest("Task not found");

        if (!await _acessCheck.IsTaskAcessibleAsync(taskId))
            return Unauthorized("Acess denied");

        return Ok(task);
    }
    
    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> Create(TaskDto entityDto)
    {
        if (!await _existCheck.DoesBoxExistAsync(entityDto.ParrentBoxId))
            return BadRequest("Task box not found");

        if (!await _acessCheck.IsBoxAcessibleAsync(entityDto.ParrentBoxId))
            return Unauthorized("Acess denied");

        var entity = await _taskRepository.CreateTaskAsync(entityDto);

        return CreatedAtAction("ViewTask", new { entity.Id }, entity);
    }

    [HttpPatch]
    [Route("Update")]
    public async Task<IActionResult> Update(int taskId, TaskDto entityDto)
    {
        var task = await _taskRepository.GetTaskByIdAsync(taskId);

        if (task == null)
            return BadRequest("Task not found");

        if (!await _existCheck.DoesBoxExistAsync(entityDto.ParrentBoxId))
                return BadRequest("Task box not found");

        if (!await _acessCheck.IsBoxAcessibleAsync(entityDto.ParrentBoxId))
            return Unauthorized("Acess denied");

        await _taskRepository.UpdateTaskAsync(task, entityDto);
        return NoContent();
    }

    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> Delete(int taskId)
    {
        var task = await _taskRepository.GetTaskByIdAsync(taskId);

        if (task == null)
            return BadRequest("Task not found");

        if (await _acessCheck.IsTaskAcessibleAsync(taskId))
            return Unauthorized("Acess denied");

        var result = _taskRepository.DeleteTaskAsync(task);
        return NoContent();
    }
}
