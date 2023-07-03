using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Dto.Receive.Group;
using ToDoList.Application.Dto.Receive.TaskBox;
using ToDoList.Application.Interfaces;
using ToDoList.Domain.Roles;

namespace ToDoList.API.Controllers;


[ApiController]
[Authorize]
[Route("api/[controller]")]
public class TaskBoxesController : ControllerBase
{
    private ITaskBoxRepository _taskBoxRepository;

    public TaskBoxesController(
        ITaskBoxRepository taskBoxRepository)
    {
        _taskBoxRepository = taskBoxRepository;
    }


    [HttpGet]
    [Authorize(ApiUserRoles.Admin)]
    [Route("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var taskBoxes = await _taskBoxRepository.GetAllTaskBoxesAsync();
        return Ok(taskBoxes);
    }


    [HttpGet]
    [Route("GetRoot")]
    public async Task<IActionResult> GetRoot(ReceiveGroupIdDto groupId)
    {
        var rootBoxes = await _taskBoxRepository.GetRootTaskBoxesAsync(groupId);

        return Ok(rootBoxes);
    }

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> Get(ReceiveTaskBoxIdDto taskBoxId)
    {
        var box = await _taskBoxRepository.GetTaskBoxByIdAsync(taskBoxId);

        return Ok(box);
    }


    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> Create(ReceiveTaskBoxDto taskBoxDto)
    {
        var entity = await _taskBoxRepository.CreateTaskBoxAsync(taskBoxDto);
        return CreatedAtAction("Get", new { entity.Id }, entity);
    }


    [HttpPatch]
    [Route("Update")]
    public async Task<IActionResult> Update(ReceiveUpdateTaskBoxDto taskBoxDto)
    {
        await _taskBoxRepository.UpdateTaskBoxAsync(taskBoxDto);

        return NoContent();
    }


    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> Delete(ReceiveTaskBoxIdDto taskBoxId)
    {
        await _taskBoxRepository.DeleteTaskBoxAsync(taskBoxId);
        return NoContent();
    }
}
