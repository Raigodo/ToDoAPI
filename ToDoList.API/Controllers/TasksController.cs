using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Dto.Receive.Task;
using ToDoList.Application.Interfaces;

namespace ToDoList.API.Controllers;


[ApiController]
[Authorize]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private ITaskRepository _taskRepository;

    public TasksController(
        ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    [HttpGet]
    [Route("Inspect")]
    public async Task<IActionResult> Inspect(ReceiveTaskIdDto taskId)
    {
        var task = await _taskRepository.GetTaskByIdAsync(taskId);

        return Ok(task);
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> Create(ReceiveTaskDto taskDto)
    {
        var entity = await _taskRepository.CreateTaskAsync(taskDto);

        return CreatedAtAction("ViewTask", new { entity.Id }, entity);
    }

    [HttpPatch]
    [Route("Update")]
    public async Task<IActionResult> Update(ReceiveUpdateTaskDto taskDto)
    {
        await _taskRepository.UpdateTaskAsync(taskDto);
        return NoContent();
    }

    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> Delete(ReceiveTaskIdDto taskId)
    {
        var result = _taskRepository.DeleteTaskAsync(taskId);
        return NoContent();
    }
}
