using Microsoft.AspNetCore.Mvc;
using ToDoList.API.Domain.Entities;

namespace ToDoList.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class TaskSetController : ControllerBase
{
    
    [HttpGet]
    [Route("GetAll")]
    public IActionResult GetAllSets()
    {
        return Ok();
    }

    [HttpGet]
    [Route("Get")]
    public IActionResult GetTaskSetContent(int id)
    {
        return Ok();
    }

    
    [HttpPost]
    [Route("Create")]
    public IActionResult PostTaskSet(int parrentId, TaskBoxEntity entity)
    {
        return CreatedAtAction("GetTaskSetContent", parrentId, entity);
    }

    [HttpPost]
    [Route("CreateRoot")]
    public IActionResult PostRootTaskSet(TaskBoxEntity entity)
    {
        return CreatedAtAction("GetAllSets", entity);
    }
    

    [HttpPatch]
    [Route("PatchTaskSet")]
    public IActionResult PatchTaskSet(TaskEntity entity)
    {
        return NoContent();
    }
    

    [HttpDelete]
    [Route("Delete")]
    public IActionResult DeleteTaskSet(int id)
    {
        return NoContent();
    }
}
