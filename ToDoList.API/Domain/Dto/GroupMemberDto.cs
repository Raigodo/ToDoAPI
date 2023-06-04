using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using ToDoList.API.Domain.Roles;
using ToDoList.API.Migrations;

namespace ToDoList.API.Domain.Dto
{
    public class GroupMemberDto
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public int GroupId { get; set; }

        public string? Role { get; set; } = GroupMemberRoles.Member;
    }
}
