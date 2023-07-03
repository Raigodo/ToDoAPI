using System.ComponentModel.DataAnnotations;

namespace ToDoList.Application.Dto.Receive.TaskBox;

public class ReceiveTaskBoxIdDto
{
    [Required]
    public int Id { get; set; }
}
