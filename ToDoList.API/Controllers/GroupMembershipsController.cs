using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Dto.Receive.Member;
using ToDoList.Application.Services.Tasks;

namespace ToDoList.API.Controllers;


[ApiController]
//[Authorize]
[Route("api/[controller]")]
public class GroupMembershipsController : ControllerBase
{
    private MembersService _memberService;

    public GroupMembershipsController(
        MembersService memberService)
    {
        _memberService = memberService;
    }

    [HttpGet]
    [Route("GetMember")]
    public async Task<IActionResult> GetMember(string userId, int groupId)
    {
        return Ok();
    }

    [HttpPost]
    [Route("AddMember")]
    public async Task<IActionResult> AddMember(ReceiveMemberDto memberDto)
    {
        return Ok();
    }

    [HttpPatch]
    [Route("Update")]
    public async Task<IActionResult> Update(ReceiveMemberDto memberDto)
    {
        return Ok();
    }


    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> Delete(string userId, int groupId)
    {
        return Ok();
    }
}