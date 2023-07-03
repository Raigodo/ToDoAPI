using System.ComponentModel.DataAnnotations;
using ToDoList.Domain.Roles;

namespace ToDoList.Application.Dto.Receive.Member;

public class ReceiveMemberDto
{
    [Required]
    public string UserId { get; set; }
    [Required]
    public int GroupId { get; set; }

    public string Role { get; set; } = GroupMemberRoles.Member;
}
