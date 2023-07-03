using System.ComponentModel.DataAnnotations;

namespace ToDoList.Application.Dto.Receive.Group;

public class ReceiveGroupIdDto
{
    [Required]
    public int Id { get; set; }
}
