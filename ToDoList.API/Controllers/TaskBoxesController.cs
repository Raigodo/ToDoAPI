using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Dto.Receive.TaskBox;
using ToDoList.Application.Services.Tasks;

namespace ToDoList.API.Controllers;


[ApiController]
//[Authorize]
[Route("api/[controller]")]
public class TaskBoxesController : ControllerBase
{
    private TaskBoxesService _taskBoxService;

    public TaskBoxesController(
        TaskBoxesService taskBoxService)
    {
        _taskBoxService = taskBoxService;
    }

    [HttpGet]
    [Route("Get/{taskBoxId}")]
    public async Task<IActionResult> Get(int taskBoxId)
    {
        return Ok();
    }


    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> Create(ReceiveTaskBoxDto taskBoxDto)
    {
        return Ok();
    }


    [HttpPatch]
    [Route("Update")]
    public async Task<IActionResult> Update(ReceiveUpdateTaskBoxDto taskBoxDto)
    {
        return Ok();
    }


    [HttpDelete]
    [Route("Delete/{taskBoxId}")]
    public async Task<IActionResult> Delete(int taskBoxId)
    {
        return Ok();
    }
}
