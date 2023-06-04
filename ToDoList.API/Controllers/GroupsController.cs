using Microsoft.AspNetCore.Mvc;
using ToDoList.API.Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using ToDoList.API.Services.Check;
using ToDoList.API.DAL.Interfaces;

namespace ToDoList.API.Controllers;


[ApiController]
[Authorize]
[Route("api/[controller]")]
public class GroupsController : ControllerBase
{
    private IAcessGuardService _acessCheck;
    private IGroupRepository _groupRepository;

    public GroupsController(
        IAcessGuardService acessCheck,
        IGroupRepository groupRepository)
    {
        _acessCheck = acessCheck;
        _groupRepository = groupRepository;
    }


    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> Get(int groupId)
    {
        var group = _groupRepository.GetGroupByIdAsync(groupId);

        if (group == null)
            return BadRequest("Group not found");

        if (!(await _acessCheck.IsGroupAcessibleAsync(groupId)))
            return Unauthorized("Acess denied");

        return Ok(group);
    }


    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> Create(GroupDto entityDto)
    {
        var group = await _groupRepository.CreateGroupAsync(entityDto);

        return CreatedAtAction("Get", new { group.Id }, group);
    }


    [HttpPatch]
    [Route("Update")]
    public async Task<IActionResult> Update(int groupId, GroupDto entityDto)
    {
        var group = await _groupRepository.GetGroupByIdAsync(groupId);

        if (group == null)
            return BadRequest("Group not found");

        if (!(await _acessCheck.IsGroupAcessibleAsync(groupId)))
            return Unauthorized("Acess denied");

        await _groupRepository.UpdateGroupAsync(group, entityDto);
        return NoContent();
    }


    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> Delete(int groupId)
    {
        var group = await _groupRepository.GetGroupByIdAsync(groupId);

        if (group == null)
            return BadRequest("Group not found");

        if (!(await _acessCheck.IsGroupAcessibleAsync(groupId)))
            return Unauthorized("Acess denied");

        await _groupRepository.DeleteGroupAsync(group);
        return NoContent();
    }
}
