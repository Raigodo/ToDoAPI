using System.ComponentModel.DataAnnotations;
using ToDoList.API.Domain.Roles;

namespace ToDoList.API.Domain.Dto
{
    public class GroupMemberDto
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public int GroupId { get; set; }

        public string Role { get; set; } = GroupMemberRoles.Member;
    }
}
