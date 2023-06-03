using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.API.DAL;
using ToDoList.API.Domain.Dto;
using ToDoList.API.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ToDoList.API.Services.Check;
using ToDoList.API.Domain.Roles;

namespace ToDoList.API.Controllers;


[ApiController]
[Authorize]
[Route("api/[controller]")]
public class GroupsController : ControllerBase
{
    private ApiDbContext _dbCtx;
    private UserManager<UserEntity> _userManager;
    private IAcessGuardService _acessCheck;
    private ICheckExistingRecordService _existCheck;

    public GroupsController(
        ApiDbContext appDbContext,
        UserManager<UserEntity> userManager,
        IAcessGuardService acessCheck,
        ICheckExistingRecordService existCheck)
    {
        _dbCtx = appDbContext;
        _userManager = userManager;
        _acessCheck = acessCheck;
        _existCheck = existCheck;
    }


    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> Get(int groupId)
    {
        var group = await _dbCtx.ApiGroups
            .Include(g => g.MembersInGroup)
            .Include(g => g.AcessibleBoxes)
            //TODO include subfolders and tasks
            .FirstOrDefaultAsync(g => g.Id == groupId);

        if (group == null)
            return BadRequest("Invalid Id");

        if (!(await _acessCheck.IsGroupAcessibleAsync(groupId)))
            return Unauthorized("Acess denied");


        return Ok(group);
    }


    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> Create(UserGroupDto entityDto)
    {
        var group = new GroupEntity()
        {
            Title = entityDto.Title,
            Description = entityDto.Description
        };
        await _dbCtx.ApiGroups.AddAsync(group);
        await _dbCtx.SaveChangesAsync();

        var groupMember = new GroupsUsersEntity {
            UserId = _userManager.GetUserId(User),
            GroupId = group.Id,
            Role = GroupMemberRoles.Admin,
        };
        await _dbCtx.ApiGroupsUsers.AddAsync(groupMember);
        await _dbCtx.SaveChangesAsync();

        return CreatedAtAction("Get", new { group.Id }, group);
    }


    [HttpPatch]
    [Route("Update")]
    public async Task<IActionResult> Update(int groupId, UserGroupDto entityDto)
    {
        var group = await _dbCtx.ApiGroups.FirstOrDefaultAsync(g => g.Id == groupId);
        if (group == null)
            return BadRequest("Invalid Id");

        if (!(await _acessCheck.IsGroupAcessibleAsync(groupId)))
            return Unauthorized("Acess denied");


        group.Title = entityDto.Title;
        group.Description = entityDto.Description;

        await _dbCtx.SaveChangesAsync();
        return NoContent();
    }


    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> Delete(int groupId)
    {
        var group = await _dbCtx.ApiGroups.FirstOrDefaultAsync(g => g.Id == groupId);
        if (group == null)
            return BadRequest("Invalid Id");

        if (!(await _acessCheck.IsGroupAcessibleAsync(groupId)))
            return Unauthorized("Acess denied");

        
        _dbCtx.ApiGroups.Remove(group);
        await _dbCtx.SaveChangesAsync();
        return NoContent();
    }
}
