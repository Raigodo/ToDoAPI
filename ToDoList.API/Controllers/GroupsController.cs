using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Dto.Receive.Group;
using ToDoList.Application.Interfaces;

namespace ToDoList.API.Controllers;


[ApiController]
[Authorize]
[Route("api/[controller]")]
public class GroupsController : ControllerBase
{
    private IGroupRepository _groupRepository;

    public GroupsController(
        IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }


    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> Get(ReceiveGroupIdDto groupId)
    {
        var group = await _groupRepository.GetGroupByIdAsync(groupId);
        return Ok(group);
    }


    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> Create(ReceiveGroupDto groupDto)
    {
        var group = await _groupRepository.CreateGroupAsync(groupDto);
        return CreatedAtAction("Get", new { group.Id }, group);
    }


    [HttpPatch]
    [Route("Update")]
    public async Task<IActionResult> Update(ReceiveUpdateGroupDto groupDto)
    {
        await _groupRepository.UpdateGroupAsync(groupDto);
        return NoContent();
    }


    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> Delete(ReceiveGroupIdDto groupId)
    {
        await _groupRepository.DeleteGroupAsync(groupId);
        return NoContent();
    }
}
