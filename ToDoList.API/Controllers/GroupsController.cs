using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Dto.Receive.Group;
using ToDoList.Application.Services.Tasks;

namespace ToDoList.API.Controllers;


[ApiController]
//[Authorize]
[Route("api/[controller]")]
public class GroupsController : ControllerBase
{
    private GroupsService _groupsService;

    public GroupsController(
        GroupsService groupService)
    {
        _groupsService = groupService;
    }


    [HttpGet]
    [Route("Get/{groupId}")]
    public async Task<IActionResult> Get(int groupId)
    {
        return Ok();
    }

    [HttpGet]
    [Route("Get/{groupId}/ContentRoot")]
    public async Task<IActionResult> ContentRoot(int groupId)
    {
        return Ok();
    }
    [HttpGet]
    [Route("Get/{groupId}/Members")]
    public async Task<IActionResult> Members(int groupId)
    {
        return Ok();
    }


    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> Create(ReceiveGroupDto groupDto)
    {
        return Ok();
    }


    [HttpPatch]
    [Route("Update")]
    public async Task<IActionResult> Update(ReceiveUpdateGroupDto groupDto)
    {
        return Ok();
    }


    [HttpDelete]
    [Route("Delete/{groupId}")]
    public async Task<IActionResult> Delete(int groupId)
    {
        return Ok();
    }
}
