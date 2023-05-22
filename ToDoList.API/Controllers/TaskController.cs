using Microsoft.AspNetCore.Mvc;
using ToDoList.API.Domain.Entities;

namespace ToDoList.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        
        [HttpGet]
        [Route("Get")]
        public IActionResult GetTask(int id)
        {
            return Ok();
        }
        
        [HttpPost]
        [Route("Post")]
        public IActionResult PostTask(int parrentId)
        {
            var task = new TaskEntity()
            {
                Id = 0
            };
            return CreatedAtAction("GetTask", task.Id, task);
        }

        [HttpPatch]
        [Route("Patch")]
        public IActionResult PatchTask(int id, TaskEntity entity)
        {
            return NoContent();
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteTask(int id)
        {
            return NoContent();
        }
        
    }
}
