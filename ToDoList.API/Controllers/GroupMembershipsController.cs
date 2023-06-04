using Microsoft.AspNetCore.Mvc;
using ToDoList.API.Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using ToDoList.API.Services.Check;
using ToDoList.API.DAL.Interfaces;

namespace ToDoList.API.Controllers;


[ApiController]
[Authorize(Roles = "ApiUser,ApiAdmin")]
[Route("api/[controller]")]
public class GroupMembershipController : ControllerBase
{
    private IAcessGuardService _acessCheck;
    private IGroupMembershipRepository _memberRepository;
    private IGroupRepository _groupRepository;
    private IUserRepository _userRepository;

    public GroupMembershipController(
        IAcessGuardService acessCheck,
        IGroupMembershipRepository memberRepository,
        IGroupRepository groupRepository,
        IUserRepository userRepository)
    {
        _acessCheck = acessCheck;
        _memberRepository = memberRepository;
        _groupRepository = groupRepository;
        _userRepository = userRepository;
    }


    [HttpGet]
    [Route("GetMembers")]
    public async Task<IActionResult> GetMembers(int groupId)
    {
        var group = await _groupRepository.GetGroupByIdAsync(groupId);

        if (group == null)
            return BadRequest("Group not found");

        if (!(await _acessCheck.IsGroupAcessibleAsync(groupId)))
            return Unauthorized("Acess denied");

        var memberList = await _memberRepository.GetAllGroupMembersAsync(group);

        return Ok(memberList);
    }

    [HttpPost]
    [Route("AddMember")]
    public async Task<IActionResult> AddMember(GroupMemberDto memberDto)
    {
        var user = await _userRepository.GetUserByIdAsync(memberDto.UserId);
        var group = await _groupRepository.GetGroupByIdAsync(memberDto.GroupId);

        if (user == null) return BadRequest("User not found");
        if (group == null) return BadRequest("Group not found");

        if (!await _acessCheck.IsGroupAcessibleAsync(memberDto.GroupId))
            return Unauthorized("Acess denied");

        var member = await _memberRepository.AddMemberAsync(user, group);

        return CreatedAtAction("GetMembers", new { member.GroupId }, member);
    }

    [HttpPatch]
    [Route("Update")]
    public async Task<IActionResult> Update(GroupMemberDto entityDto)//currently useless
    {
        var user = await _userRepository.GetUserByIdAsync(entityDto.UserId);
        var group = await _groupRepository.GetGroupByIdAsync(entityDto.GroupId);
        if (user == null || group == null) return BadRequest("Member not found");

        var member = await _memberRepository.GetMemberAsync(user, group);
        if ( member == null) return BadRequest("Member not found");

        if (!(await _acessCheck.IsGroupMemberAcessibleAsync(entityDto.UserId, entityDto.GroupId)))
            return Unauthorized("Acess denied");

        await _memberRepository.UpdateMemberAsync(member, entityDto);
        return NoContent();
    }


    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> Delete(string userId, int groupId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        var group = await _groupRepository.GetGroupByIdAsync(groupId);
        if (user == null || group == null) return BadRequest("Member not found");

        var member = await _memberRepository.GetMemberAsync(user, group);
        if (member == null) return BadRequest("Member not found");

        if (!(await _acessCheck.IsGroupMemberAcessibleAsync(userId: userId, groupId: groupId)))
            return Unauthorized("Acess denied");

        await _memberRepository.RemoveGroupMemberAsync(member);
        return NoContent();
    }
}