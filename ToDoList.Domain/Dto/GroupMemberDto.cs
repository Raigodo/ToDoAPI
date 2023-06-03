using System.ComponentModel.DataAnnotations;

namespace ToDoList.API.Domain.Dto
{
    public class GroupMemberDto
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public int GroupId { get; set; }
    }
}
