using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Dto.Receive.Group;
using ToDoList.Application.Dto.Receive.Member;
using ToDoList.Application.Interfaces;

namespace ToDoList.API.Controllers;


[ApiController]
//[Authorize]
[Route("api/[controller]")]
public class GroupMembershipController : ControllerBase
{
    private IGroupMembershipRepository _memberRepository;

    public GroupMembershipController(
        IGroupMembershipRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }


    [HttpGet]
    [Route("GetMembers")]
    public async Task<IActionResult> GetMembers(ReceiveGroupIdDto groupId)
    {
        var memberList = await _memberRepository.GetAllGroupMembersAsync(groupId);

        return Ok(memberList);
    }

    [HttpPost]
    [Route("AddMember")]
    public async Task<IActionResult> AddMember(ReceiveMemberDto memberDto)
    {
        var member = await _memberRepository.AddMemberAsync(memberDto);

        return CreatedAtAction("GetMembers", new { member.GroupId }, member);
    }

    [HttpPatch]
    [Route("Update")]
    public async Task<IActionResult> Update(ReceiveMemberDto memberDto)
    {
        await _memberRepository.UpdateMemberAsync(memberDto);
        return NoContent();
    }


    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> Delete(ReceiveMemberIdDto memberId)
    {
        await _memberRepository.RemoveGroupMemberAsync(memberId);
        return NoContent();
    }
}