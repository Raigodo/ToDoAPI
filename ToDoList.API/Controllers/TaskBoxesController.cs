using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.API.DAL;
using ToDoList.API.Domain.Dto;
using ToDoList.API.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ToDoList.API.Services.Check;
using ToDoList.API.DAL.Interfaces;
using ToDoList.API.DAL.Repositories;

namespace ToDoList.API.Controllers;


[ApiController]
[Authorize(Roles = "ApiUser,ApiAdmin")]
[Route("api/[controller]")]
public class TaskBoxesController : ControllerBase
{
    private IAcessGuardService _acessCheck;
    private ICheckExistingRecordService _existCheck;
    private ITaskBoxRepository _taskBoxRepository;
    private IGroupRepository _groupRepository;

    public TaskBoxesController(
        IAcessGuardService acessCheck,
        ICheckExistingRecordService existCheck,
        ITaskBoxRepository taskBoxRepository,
        IGroupRepository groupRepository)
    {
        _acessCheck = acessCheck;
        _existCheck = existCheck;
        _taskBoxRepository = taskBoxRepository;
        _groupRepository = groupRepository;
    }

    [HttpGet]
    [Authorize]
    [Route("GetFoldersInGroup")]
    public async Task<IActionResult> GetFoldersInGroup(int groupId)
    {
        var group = await _groupRepository.GetGroupByIdAsync(groupId);

        if (group == null)
            return BadRequest("Group not found");

        if (!await _acessCheck.IsGroupAcessibleAsync(groupId))
            return Unauthorized("Acess denied");

        var rootBoxes = await _taskBoxRepository.GetGroupRootTaskBoxesAsync(group);

        return Ok(rootBoxes);
    }

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> Get(int boxId)
    {
        var box = await _taskBoxRepository.GetTaskBoxByIdAsync(boxId);

        if (box == null)
            return BadRequest("Task box not found");

        if (!(await _acessCheck.IsBoxAcessibleAsync(boxId)))
            return Unauthorized("Acess denied");

        return Ok(box);
    }

    
    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> Create(TaskBoxDto entityDto)
    {
        if (!await _existCheck.DoesGroupExistAsync(entityDto.AssociatedGroupId))
            return BadRequest("Group not found");

        if (entityDto.ParrentBoxId != null && !await _existCheck.DoesBoxExistAsync((int)entityDto.ParrentBoxId))
            return BadRequest("Parrent task box not found");

        if (!(await _acessCheck.IsGroupAcessibleAsync(entityDto.AssociatedGroupId)))
            return Unauthorized("Acess denied");

        var entity = await _taskBoxRepository.CreateTaskBoxAsync(entityDto);
        return CreatedAtAction("Get", new { entity.Id }, entity);
    }
    

    [HttpPatch]
    [Route("Update")]
    public async Task<IActionResult> Update(int boxId, TaskBoxDto entityDto)
    {
        var box = await _taskBoxRepository.GetTaskBoxByIdAsync(boxId);

        if (box == null)
            return BadRequest("Task box not found");

        if (!await _existCheck.DoesGroupExistAsync(entityDto.AssociatedGroupId))
            return BadRequest("Group not found");

        if (entityDto.ParrentBoxId != null && await _existCheck.DoesBoxExistAsync((int)entityDto.ParrentBoxId))
            return BadRequest("Parrent task box not found");

        if (!(await _acessCheck.IsGroupAcessibleAsync(entityDto.AssociatedGroupId)))
            return Unauthorized("Acess denied");

        await _taskBoxRepository.UpdateTaskBoxAsync(box, entityDto);
        return NoContent();
    }
    

    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> Delete(int boxId)
    {
        var box = await _taskBoxRepository.GetTaskBoxByIdAsync(boxId);

        if (box == null)
            return BadRequest("Task box not found");

        if (await _acessCheck.IsGroupAcessibleAsync(boxId))
            return Unauthorized("Acess denied");

        await _taskBoxRepository.DeleteTaskBoxAsync(box);
        return NoContent();
    }
}
