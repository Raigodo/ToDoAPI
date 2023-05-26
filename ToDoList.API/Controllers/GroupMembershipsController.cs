using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using ToDoList.API.DAL;
using ToDoList.API.Domain.Dto;
using ToDoList.API.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace ToDoList.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class GroupMembershipController : ControllerBase
{
    private ApiDbContext _dbCtx;

    public GroupMembershipController(ApiDbContext appDbContext)
    {
        _dbCtx = appDbContext;
    }


    [HttpGet]
    [Route("GetMembers")]
    public async Task<IActionResult> GetMembers(int groupId)
    {
        var doesGroupExists = await _dbCtx.ApiGroups
            .AnyAsync(g => g.Id == groupId);
        if (!doesGroupExists)
            return BadRequest("Invalid Id");

        var memberList = await _dbCtx.ApiGroupsUsers
            .Where(gu => gu.GroupId == groupId)
            .ToListAsync();

        return Ok(memberList);
    }

    [HttpPost]
    [Route("AddMember")]
    public async Task<IActionResult> AddMember(GroupMemberDto entityDto)
    {
        var doesUserExists = await _dbCtx.ApiUsers.AnyAsync(u=>u.Id == entityDto.UserId);
        var doesGroupExists = await _dbCtx.ApiGroups.AnyAsync(g => g.Id == entityDto.GroupId);
        var doesMemberAlreadyExists = await _dbCtx.ApiGroupsUsers.AnyAsync(m => m.GroupId == entityDto.GroupId && m.UserId == entityDto.UserId);

        if (!doesGroupExists || !doesUserExists || doesMemberAlreadyExists)
            return BadRequest("Invalid Id");

        var member = new GroupMemberEntity()
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
        var groupUser = await _dbCtx.ApiGroupsUsers.FirstOrDefaultAsync(g => g.UserId == entityDto.UserId && g.GroupId == entityDto.GroupId);
        if (groupUser == null)
            return BadRequest("Invalid Id");

        groupUser.GroupId = entityDto.GroupId;
        groupUser.UserId = entityDto.UserId;

        await _dbCtx.SaveChangesAsync();
        return NoContent();
    }


    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> Delete(int userId, int groupId)
    {
        var groupUser = _dbCtx.ApiGroupsUsers.FirstOrDefault(gu => gu.UserId == userId && gu.GroupId == groupId);
        if (groupUser == null)
            return BadRequest("Invalid Id");

        _dbCtx.ApiGroupsUsers.Remove(groupUser);
        await _dbCtx.SaveChangesAsync();
        return NoContent();
    }
}