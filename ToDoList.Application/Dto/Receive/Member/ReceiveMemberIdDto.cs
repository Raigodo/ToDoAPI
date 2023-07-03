using System.ComponentModel.DataAnnotations;

namespace ToDoList.Application.Dto.Receive.Member;

public class ReceiveMemberIdDto
{
    [Required]
    public string UserId { get; set; }
    [Required]
    public int GroupId { get; set; }
}
