using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Dto.Receive.Task;
using ToDoList.Application.Exceptions;
using ToDoList.Application.Services.Tasks;

namespace ToDoList.API.Controllers;


[ApiController]
//[Authorize]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private TasksService _tasksService;

    public TasksController(
        TasksService tasksService)
    {
        _tasksService = tasksService;
    }

    [HttpGet]
    [Route("Get/{taskId}")]
    public async Task<IActionResult> Get(int taskId)
    {
        return Ok();
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> Create(ReceiveTaskDto taskDto)
    {
        return Ok();
    }

    [HttpPatch]
    [Route("Update")]
    public async Task<IActionResult> Update(ReceiveUpdateTaskDto taskDto)
    {
        return Ok();
    }

    [HttpDelete]
    [Route("Delete/{taskId}")]
    public async Task<IActionResult> Delete(int taskId)
    {
        return Ok();
    }
}
