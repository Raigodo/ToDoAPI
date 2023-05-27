using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.API.DAL;
using ToDoList.API.Domain.Dto;
using ToDoList.API.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;
using ToDoList.API.Services.Check;

namespace ToDoList.API.Controllers;


[ApiController]
[Authorize(Roles = "ApiUser,ApiAdmin")]
[Route("api/[controller]")]
public class GroupMembershipController : ControllerBase
{
    private ApiDbContext _dbCtx;
    private UserManager<UserEntity> _userManager;
    private IAcessGuardService _acessCheck;
    private ICheckExistingRecordService _existCheck;

    public GroupMembershipController(
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
    [Route("GetMembers")]
    public async Task<IActionResult> GetMembers(int groupId)
    {
        if (!(await _existCheck.DoesGroupExistAsync(groupId)))
            return BadRequest("Invalid Id");

        if (!(await _acessCheck.IsGroupAcessibleAsync(groupId)))
            return Unauthorized("Acess denied");


        var memberList = await _dbCtx.ApiGroupsUsers
            .Where(gu => gu.GroupId == groupId)
            .ToListAsync();

        return Ok(memberList);
    }

    [HttpPost]
    [Route("AddMember")]
    public async Task<IActionResult> AddMember(GroupMemberDto entityDto)
    {
        if (!(await _existCheck.DoesGroupMemberExistAsync(userId: entityDto.UserId, groupId: entityDto.GroupId)))
            return BadRequest("Invalid Id");

        if (!(await _acessCheck.IsGroupMemberAcessibleAsync(userId: entityDto.UserId, groupId: entityDto.GroupId)))
            return Unauthorized("Acess denied");


        var member = new GroupsUsersEntity()
        {
            UserId = entityDto.UserId,
            GroupId = entityDto.GroupId,
        }; 
        await _dbCtx.ApiGroupsUsers.AddAsync(member);
        await _dbCtx.SaveChangesAsync();

        return CreatedAtAction("GetMembers", new { member.GroupId }, member);
    }

    [HttpPatch]
    [Route("Update")]
    public async Task<IActionResult> Update(GroupMemberDto entityDto)//currently useless
    {
        if (!(await _existCheck.DoesGroupMemberExistAsync(userId: entityDto.UserId, groupId: entityDto.GroupId)))
            return BadRequest("Invalid Id");

        if (!(await _acessCheck.IsGroupMemberAcessibleAsync(userId: entityDto.UserId, groupId: entityDto.GroupId)))
            return Unauthorized("Acess denied");


        var groupUser = await _dbCtx.ApiGroupsUsers.FirstOrDefaultAsync(g => g.UserId == entityDto.UserId && g.GroupId == entityDto.GroupId);

        groupUser.GroupId = entityDto.GroupId;
        groupUser.UserId = entityDto.UserId;

        await _dbCtx.SaveChangesAsync();
        return NoContent();
    }


    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> Delete(string userId, int groupId)
    {
        if (!(await _existCheck.DoesGroupMemberExistAsync(userId: userId, groupId: groupId)))
            return BadRequest("Invalid Id");

        if (!(await _acessCheck.IsGroupMemberAcessibleAsync(userId: userId, groupId: groupId)))
            return Unauthorized("Acess denied");


        var groupUser = _dbCtx.ApiGroupsUsers.FirstOrDefault(gu => gu.UserId == userId && gu.GroupId == groupId);

        _dbCtx.ApiGroupsUsers.Remove(groupUser);
        await _dbCtx.SaveChangesAsync();
        return NoContent();
    }
}