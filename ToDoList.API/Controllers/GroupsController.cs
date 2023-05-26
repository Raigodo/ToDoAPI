using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.API.DAL;
using ToDoList.API.Domain.Dto;
using ToDoList.API.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ToDoList.API.Services.Check;

namespace ToDoList.API.Controllers;


[ApiController]
[Authorize]
[Route("api/[controller]")]
public class GroupsController : ControllerBase
{
    private ApiDbContext _dbCtx;
    private UserManager<IdentityUser> _userManager;
    private IAcessGuardService _acessCheck;
    private ICheckExistingRecordService _existCheck;

    public GroupsController(
        ApiDbContext appDbContext,
        UserManager<IdentityUser> userManager,
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
        if (!(await _existCheck.DoesGroupExistAsync(groupId)))
            return BadRequest("Invalid Id");

        if (await _acessCheck.IsGroupAcessibleAsync(groupId))
            return Unauthorized("Acess denied");


        var group = await _dbCtx.ApiGroups
            .Include(u => u.MembersInGroup)
            .Include(u => u.AcessibleBoxes)
            .FirstOrDefaultAsync(g => g.Id == groupId);

        return Ok(group);
    }


    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> Create(UserGroupDto entityDto)
    {
        var user = User;
        var group = new GroupEntity()
        {
            Title = entityDto.Title,
            Description = entityDto.Description
        };
        await _dbCtx.ApiGroups.AddAsync(group);
        await _dbCtx.SaveChangesAsync();

        group.MembersInGroup.Add(new GroupsUsersEntity
        {
            UserId = _userManager.GetUserId(User),
            GroupId = group.Id
        });

        return CreatedAtAction("Get", new { group.Id }, group);
    }


    [HttpPatch]
    [Route("Update")]
    public async Task<IActionResult> Update(int groupId, UserGroupDto entityDto)
    {
        if (!(await _existCheck.DoesGroupExistAsync(groupId)))
            return BadRequest("Invalid Id");

        if (await _acessCheck.IsGroupAcessibleAsync(groupId))
            return Unauthorized("Acess denied");


        var group = await _dbCtx.ApiGroups.FirstOrDefaultAsync(g => g.Id == groupId);
        
        group.Title = entityDto.Title;
        group.Description = entityDto.Description;

        await _dbCtx.SaveChangesAsync();
        return NoContent();
    }


    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> Delete(int groupId)
    {
        if (!(await _existCheck.DoesGroupExistAsync(groupId)))
            return BadRequest("Invalid Id");

        if (await _acessCheck.IsGroupAcessibleAsync(groupId))
            return Unauthorized("Acess denied");


        var group = _dbCtx.ApiGroups.FirstOrDefault(g => g.Id == groupId);
        
        _dbCtx.ApiGroups.Remove(group);
        await _dbCtx.SaveChangesAsync();
        return NoContent();
    }
}
