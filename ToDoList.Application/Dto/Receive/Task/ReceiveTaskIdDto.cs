using System.ComponentModel.DataAnnotations;

namespace ToDoList.Application.Dto.Receive.Task;

public class ReceiveTaskIdDto
{
    [Required]
    public int Id { get; set; }
}
